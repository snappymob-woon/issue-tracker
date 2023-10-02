using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<int>, IAuditable, ISoftDelete
{
    public List<Issue> AuthoredIssues { get; set; } = new List<Issue>();
    public List<Issue> AssignedIssues { get; set; } = new List<Issue>();
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public bool IsDeleted { get; set; }
}