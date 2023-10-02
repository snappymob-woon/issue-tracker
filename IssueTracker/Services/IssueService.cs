using System.Net.Mail;
using System.Runtime.CompilerServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
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

    public async Task<List<IssueSummaryViewModel>> GetIssues(IssueTracker.Pages.IndexModel.FilterInputModel? filterInput)
    {
        var query = _context.Issues.AsQueryable();

        if (filterInput.IssueStatus.HasValue)
        {
            query = query.Where(x => x.Status == filterInput.IssueStatus.Value);
        }

        if (!string.IsNullOrWhiteSpace(filterInput.Query))
        {
            query = query.Where(x => x.Title.ToLower().Contains(filterInput.Query.ToLower()) || x.Description.ToLower().Contains(filterInput.Query.ToLower()));
        }

        if (filterInput.AssigneeId.HasValue)
        {
            query = query.Where(x => x.Assignees.Any(assignee => assignee.Id == filterInput.AssigneeId.Value));
        }

        if (filterInput.StartDate.HasValue)
        {
            query = query.Where(x => x.CreatedDate.Date >= filterInput.StartDate.Value.Date);
        }

        if (filterInput.EndDate.HasValue)
        {
            query = query.Where(x => x.CreatedDate.Date <= filterInput.EndDate.Value.Date);
        }

        return await query
            .Include(x => x.Author)
            .Include(x => x.Assignees)
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
                                .Include(x => x.Tags)
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

        var tags = await _context.IssueTags.Where(x => cmd.TagIds.Select(y => int.Parse(y)).Contains(x.Id)).ToListAsync();
        issue.Tags = tags;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteIssue(int id)
    {
        var issue = await _context.Issues.FirstOrDefaultAsync(x => x.Id == id);

        if (issue is null)
        {
            throw new Exception("The issue is not found");
        }

        issue.IsDeleted = true;

        await _context.SaveChangesAsync();
    }
}