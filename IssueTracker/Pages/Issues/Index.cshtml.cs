using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IssueTracker.Pages.Issues;

public class IssueModel : PageModel
{
    private readonly ILogger<IssueModel> _logger;
    private readonly IHttpContextAccessor _accessor;

    public IssueModel(ILoggerFactory factory, IHttpContextAccessor accessor)
    {
        _logger = factory.CreateLogger<IssueModel>();
        _accessor = accessor;
    }

    public Issue CurrentIssue { get; set; }

    [BindProperty]
    public CreateCommentCommand NewCommentCommand { get; set; }

    public async Task<IActionResult> OnGetAsync([FromServices] IssueService service, int? id)
    {
        if (!id.HasValue)
        {
            return RedirectToPage("../Index");
        }

        CurrentIssue = await service.GetIssue(id.Value);

        return Page();
    }

    public async Task<IActionResult> OnPostCommentAsync([FromServices] CommentService service)
    {
        NewCommentCommand.AuthorId = int.Parse(_accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        await service.CreateComment(NewCommentCommand);

        return RedirectToPage("Index", new { id = NewCommentCommand.IssueId });
    }
}
