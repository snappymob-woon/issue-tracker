using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

public class EditIssueModel : PageModel
{
    [BindProperty]
    public EditIssueCommand CurrentIssue { get; set; }
    public SelectList AvailableAssignees { get; set; }
    public SelectList AvailableTags { get; set; }

    private readonly IMapper _mapper;
    private readonly ILogger<EditIssueModel> _logger;
    private readonly IssueService _issueService;
    private readonly UserService _userService;
    private readonly IssueTagService _issueTagService;

    public EditIssueModel(ILoggerFactory factory, IMapper mapper, IssueService issueService, UserService userService, IssueTagService issueTagService)
    {
        _logger = factory.CreateLogger<EditIssueModel>();
        _mapper = mapper;
        _issueService = issueService;
        _userService = userService;
        _issueTagService = issueTagService;
    }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (!id.HasValue) return RedirectToPage("../Index");

        var issue = await _issueService.GetIssue(id.Value);

        if (issue is null)
            throw new Exception("This issue is not found");

        var assignees = await _userService.GetUsersAsync();

        AvailableAssignees = new SelectList(assignees, "Id", "UserName");

        var tags = await _issueTagService.GetIssueTags();
        AvailableTags = new SelectList(tags, "Id", "Label");

        CurrentIssue = _mapper.Map<EditIssueCommand>(issue);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        CurrentIssue.Id = id.Value;
        await _issueService.UpdateIssue(CurrentIssue);

        return RedirectToPage("Detail", new { id = CurrentIssue.Id });
    }
}