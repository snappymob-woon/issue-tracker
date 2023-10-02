using AutoMapper;
using Microsoft.EntityFrameworkCore;

public class IssueTagService
{

    private readonly IMapper _mapper;
    private readonly AppDbContext _context;
    private readonly ILogger _logger;

    public IssueTagService(AppDbContext context, ILoggerFactory factory, IMapper mapper)
    {
        _context = context;
        _logger = factory.CreateLogger<IssueTagService>();
        _mapper = mapper;
    }

    public async Task<List<IssueTag>> GetIssueTags()
    {
        return await _context.IssueTags.ToListAsync();
    }
}