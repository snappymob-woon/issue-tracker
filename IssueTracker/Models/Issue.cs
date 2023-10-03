/*
Issue:
 - Status: Enum<Status>
 - Description: string
 - Assignees: List<User>
 - Comments: List<Comment>
 - Story Points: int
*/


using IssueTracker.Atrributes;

public class Issue : EntityBase<int>
{
    public enum IssueStatus
    {
        [Color("secondary")]
        Backlog,
        [Color("primary")]
        Ongoing,
        [Color("success")]
        Complete,
        [Color("danger")]
        Blocked
    }

    public User Author { get; set; }
    public IssueStatus Status { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public List<User> Assignees { get; set; } = new List<User>();
    public List<Comment> Comments { get; set; } = new List<Comment>();
    public List<IssueTag> Tags { get; set; } = new List<IssueTag>();
    public Project Project { get; set; }
    public int StoryPoints { get; set; }
}
