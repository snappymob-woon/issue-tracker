
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IssueTracker.Pages.Issues;

public class NewIssueModel : PageModel
{
    private readonly ILogger _logger;
    private readonly IssueService _issueService;
    private readonly UserService _userService;
    private readonly IssueTagService _issueTagService;
    private readonly ProjectService _projectService;
    private readonly IMapper _mapper;
    public SelectList AvailableAssignees { get; set; }
    public SelectList AvailableTags { get; set; }
    public ProjectSummaryViewModel CurrentProject { get; set; }

    [BindProperty]
    public CreateIssueCommand Issue { get; set; }

    public NewIssueModel(ILogger<NewIssueModel> logger, IMapper mapper, IssueService issueService, UserService userService, IssueTagService issueTagService, ProjectService projectService)
    {
        _logger = logger;
        _mapper = mapper;
        _issueService = issueService;
        _userService = userService;
        _issueTagService = issueTagService;
        _projectService = projectService;
    }

    public async Task<IActionResult> OnGet(int projectId)
    {
        var project = await _projectService.GetProject(projectId);
        if (project is null)
        {
            return RedirectToPage("../Index");
        }

        CurrentProject = _mapper.Map<ProjectSummaryViewModel>(project);

        var assignees = await _userService.GetUsersAsync();
        AvailableAssignees = new SelectList(assignees, "Id", "UserName");

        var tags = await _issueTagService.GetIssueTags();
        AvailableTags = new SelectList(tags, "Id", "Label");

        return Page();
    }

    public async Task<IActionResult> OnPostAsync([FromServices] IHttpContextAccessor accessor, int projectId)
    {
        Issue.AuthorId = int.Parse(accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        Issue.ProjectId = projectId;
        var id = await _issueService.CreateIssue(Issue);

        return RedirectToPage("Detail", new { id });
    }
}