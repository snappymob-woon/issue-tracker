using System.Net.Mail;
using System.Runtime.CompilerServices;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

public class IssueService
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;
    private readonly ILogger _logger;

    public IssueService(AppDbContext context, ILoggerFactory factory, IMapper mapper)
    {
        _context = context;
        _logger = factory.CreateLogger<IssueService>();
        _mapper = mapper;
    }

    public async Task<List<IssueSummaryViewModel>> GetIssues()
    {
        return await _context.Issues
            .Where(x => x.IsDeleted == false)
            .Include(x => x.Author)
            .Select(x => _mapper.Map<IssueSummaryViewModel>(x))
            .ToListAsync();
    }

    public async Task<int> CreateIssue(CreateIssueCommand cmd)
    {
        var author = await _context.Users.FindAsync(cmd.AuthorId);
        var issue = _mapper.Map<Issue>(cmd);
        issue.Author = author;

        _context.Add(issue);
        await _context.SaveChangesAsync();
        return issue.Id;
    }

    public async Task<Issue> GetIssue(int id)
    {
        var issue = await _context.Issues
                                        .Include(x => x.Comments)
                                        .Include(x => x.Author)
                                        .Include(x => x.Assignees)
                                        .FirstOrDefaultAsync(i => i.Id == id);

        if (issue is null)
        {
            throw new Exception("Issue not found.");
        }

        return issue;
    }

    public async Task UpdateIssue(EditIssueCommand cmd)
    {
        var issue = await _context.Issues.Include(x => x.Assignees).FirstOrDefaultAsync(x => x.Id == cmd.Id);


        if (issue is null)
        {
            throw new Exception("The issue is not found");
        }

        _mapper.Map(cmd, issue);

        var assignees = await _context.Users.Where(x => cmd.AssigneeIds.Select(y => int.Parse(y)).Contains(x.Id)).ToListAsync();
        issue.Assignees = assignees;

        await _context.SaveChangesAsync();
    }
}