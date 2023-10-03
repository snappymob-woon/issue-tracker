
using AutoMapper;
using Microsoft.EntityFrameworkCore;

public class UserService : BaseService<UserService>
{
    public UserService(AppDbContext context, ILoggerFactory factory, IMapper mapper) : base(context, factory, mapper)
    {
    }

    public async Task<List<UserSummaryViewModel>> GetUsersAsync()
    {
        return await _context.Users
            .Select(x => _mapper.Map<UserSummaryViewModel>(x))
            .ToListAsync();
    }
}