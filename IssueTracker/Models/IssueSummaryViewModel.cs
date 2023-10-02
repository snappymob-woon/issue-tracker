public class IssueSummaryViewModel
{
    public int Id { get; set; }
    public string AuthorName { get; set; }
    public Issue.IssueStatus Status { get; set; }
    public required string Title { get; set; }
    public string Description { get; set; }
    public int StoryPoints { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}