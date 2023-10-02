using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

public class EditIssueModel : PageModel
{
    [BindProperty]
    public EditIssueCommand CurrentIssue { get; set; }
    public SelectList AvailableAssignees { get; set; }

    private readonly IMapper _mapper;
    private readonly ILogger<EditIssueModel> _logger;
    private readonly IssueService _issueService;
    private readonly UserService _userService;

    public EditIssueModel(ILoggerFactory factory, IMapper mapper, IssueService issueService, UserService userService)
    {
        _logger = factory.CreateLogger<EditIssueModel>();
        _mapper = mapper;
        _issueService = issueService;
        _userService = userService;
    }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (!id.HasValue) return RedirectToPage("../Index");

        var issue = await _issueService.GetIssue(id.Value);

        if (issue is null)
            throw new Exception("This issue is not found");

        var assignees = await _userService.GetUsersAsync();
        foreach (var assignee in assignees)
        {
            _logger.LogInformation(assignee.UserName);
        }
        // AvailableAssignees = assignees.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.UserName }).ToList()
        AvailableAssignees = new SelectList(assignees, "Id", "UserName");

        CurrentIssue = _mapper.Map<EditIssueCommand>(issue);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        _logger.LogDebug(string.Join(',', CurrentIssue.AssigneeIds));
        CurrentIssue.Id = id.Value;
        await _issueService.UpdateIssue(CurrentIssue);

        return RedirectToPage("Index", new { id = CurrentIssue.Id });
    }
}