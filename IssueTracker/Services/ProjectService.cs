using AutoMapper;
using Microsoft.EntityFrameworkCore;

public class ProjectService : BaseService<ProjectService>
{
    public ProjectService(AppDbContext context, ILoggerFactory factory, IMapper mapper) : base(context, factory, mapper)
    {
    }

    public async Task<List<Project>> GetProjectsAsync()
    {
        return await _context.Projects.ToListAsync();
    }

    public async Task<Project?> GetProject(int id)
    {
        return await _context.Projects.Include(x => x.Issues).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> CreateProjectAsync(CreateProjectCommand cmd)
    {
        var project = _mapper.Map<Project>(cmd);

        _context.Add(project);
        await _context.SaveChangesAsync();

        return project.Id;
    }
}