public class EditIssueBase
{
    public Issue.IssueStatus Status { get; set; }
    public required string Title { get; set; }
    public string Description { get; set; }
    public int StoryPoints { get; set; }
    public IEnumerable<string> AssigneeIds { get; set; } = new List<string>();
}