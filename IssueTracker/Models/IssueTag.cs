using IssueTracker.Atrributes;

public class IssueTag : EntityBase<int>
{
    public required string Slug { get; set; }
    public required string Label { get; set; }
    public string Description { get; set; } = "";
    public string Color { get; set; } = ColorHelper.GetColorAttributeValue(BootstrapColor.Secondary);
    public List<Issue> Issues { get; set; } = new List<Issue>();
}