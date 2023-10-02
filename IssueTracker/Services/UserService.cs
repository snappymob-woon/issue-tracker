
using AutoMapper;
using Microsoft.EntityFrameworkCore;

public class UserService
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;
    private readonly ILogger _logger;

    public UserService(AppDbContext context, ILoggerFactory factory, IMapper mapper)
    {
        _context = context;
        _logger = factory.CreateLogger<IssueService>();
        _mapper = mapper;
    }

    public async Task<List<UserSummaryViewModel>> GetUsersAsync()
    {
        return await _context.Users
            .Select(x => _mapper.Map<UserSummaryViewModel>(x))
            .ToListAsync();
    }
}