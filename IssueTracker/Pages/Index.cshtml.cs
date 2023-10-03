using System.Runtime.CompilerServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.TagHelpers;

public class ProjectModel : PageModel
{
    private readonly ILogger<ProjectModel> _logger;
    private readonly IMapper _mapper;
    private readonly ProjectService _projectService;
    public List<Project> Projects { get; set; } = new List<Project>();

    public ProjectModel(IMapper mapper, ILoggerFactory factory, ProjectService projectService)
    {
        _logger = factory.CreateLogger<ProjectModel>();
        _mapper = mapper;
        _projectService = projectService;
    }

    public async Task OnGetAsync()
    {
        Projects = await _projectService.GetProjectsAsync();
    }
}