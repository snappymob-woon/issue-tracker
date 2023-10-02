using Microsoft.AspNetCore.Mvc;

public class CreateCommentCommand : EditCommentBase
{
    public int AuthorId { get; set; }
    [HiddenInput]
    public int IssueId { get; set; }
}