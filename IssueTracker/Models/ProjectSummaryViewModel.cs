public class ProjectSummaryViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public List<IssueSummaryViewModel> Issues { get; set; } = new List<IssueSummaryViewModel>();
}