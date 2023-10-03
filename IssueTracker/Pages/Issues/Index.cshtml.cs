using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IssueTracker.Pages.Issues;

public class IssueModel : PageModel
{
    private readonly ILogger<IssueModel> _logger;
    private readonly IMapper _mapper;
    private readonly IssueService _issueService;
    private readonly UserService _userService;
    private readonly ProjectService _projectService;
    public IEnumerable<SelectListItem> AvailableAssignees { get; set; }
    public ProjectSummaryViewModel CurrentProject { get; set; }

    [BindProperty(SupportsGet = true)]
    public FilterInputModel FilterInput { get; set; }

    public IssueModel(ILoggerFactory factory, IMapper mapper, IssueService issueService, UserService userService, ProjectService projectService)
    {
        _logger = factory.CreateLogger<IssueModel>();
        _mapper = mapper;
        _issueService = issueService;
        _userService = userService;
        _projectService = projectService;
    }

    public async Task<IActionResult> OnGet(int projectId)
    {
        var project = await _projectService.GetProject(projectId);
        if (project is null)
        {
            return RedirectToPage("../Index");
        }

        ViewData["Title"] = $"Issues for {project.Name}";

        CurrentProject = _mapper.Map<ProjectSummaryViewModel>(project);
        AvailableAssignees = (await _userService.GetUsersAsync()).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.UserName });
        return Page();
    }

    public class FilterInputModel
    {
        public string? Query { get; set; }
        public int? AssigneeId { get; set; }
        public Issue.IssueStatus? IssueStatus { get; set; }
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
    }
}
