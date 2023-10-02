using Microsoft.EntityFrameworkCore.Storage;

public class CreateIssueCommand : EditIssueBase
{
    public int AuthorId { get; set; }
    public IList<CreateUserCommand> Assignees { get; set; } = new List<CreateUserCommand>();
}