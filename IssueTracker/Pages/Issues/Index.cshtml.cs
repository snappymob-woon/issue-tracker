using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IssueTracker.Pages.Issues;

public class IssueModel : PageModel
{
    private readonly ILogger<IssueModel> _logger;
    private readonly IHttpContextAccessor _accessor;
    private readonly IssueService _issueService;
    private readonly CommentService _commentService;

    public IssueModel(ILoggerFactory factory, IHttpContextAccessor accessor, IssueService issueService, CommentService commentService)
    {
        _logger = factory.CreateLogger<IssueModel>();
        _accessor = accessor;
        _issueService = issueService;
        _commentService = commentService;
    }

    public Issue CurrentIssue { get; set; }

    [BindProperty]
    public CreateCommentCommand NewCommentCommand { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (!id.HasValue)
        {
            return RedirectToPage("../Index");
        }

        CurrentIssue = await _issueService.GetIssue(id.Value);

        return Page();
    }

    public async Task<IActionResult> OnPostCommentAsync()
    {
        NewCommentCommand.AuthorId = int.Parse(_accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        await _commentService.CreateComment(NewCommentCommand);

        return RedirectToPage("Index", new { id = NewCommentCommand.IssueId });
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        await _issueService.DeleteIssue(id);

        return RedirectToPage("../Index");
    }
}
