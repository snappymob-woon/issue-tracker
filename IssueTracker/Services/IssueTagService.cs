using AutoMapper;
using Microsoft.EntityFrameworkCore;

public class IssueTagService : BaseService<IssueTagService>
{
    public IssueTagService(AppDbContext context, ILoggerFactory factory, IMapper mapper) : base(context, factory, mapper)
    {
    }

    public async Task<List<IssueTag>> GetIssueTags()
    {
        return await _context.IssueTags.ToListAsync();
    }
}