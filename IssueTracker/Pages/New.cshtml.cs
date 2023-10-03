using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SQLitePCL;

public class NewProjectModel : PageModel
{
    private readonly ProjectService _projectService;

    [BindProperty]
    public CreateProjectCommand ProjectModel { get; set; }

    public NewProjectModel(ProjectService projectService)
    {
        _projectService = projectService;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        int projectId = await _projectService.CreateProjectAsync(ProjectModel);
        return RedirectToPage("Issues/Index", new { projectId });
    }
}