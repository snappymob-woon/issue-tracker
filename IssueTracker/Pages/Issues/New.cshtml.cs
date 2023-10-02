
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IssueTracker.Pages.Issues;

public class NewIssueModel : PageModel
{
    private readonly ILogger _logger;

    [BindProperty]
    public CreateIssueCommand Issue { get; set; }

    public NewIssueModel(ILogger<NewIssueModel> logger)
    {
        _logger = logger;
    }


    public async Task<IActionResult> OnPostAsync([FromServices] IssueService service, [FromServices] IHttpContextAccessor accessor)
    {
        Issue.AuthorId = int.Parse(accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        var id = await service.CreateIssue(Issue);

        return RedirectToPage("Index", new { id });
    }
}