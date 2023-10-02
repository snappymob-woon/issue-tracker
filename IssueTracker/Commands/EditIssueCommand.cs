
using Microsoft.AspNetCore.Mvc;

public class EditIssueCommand : EditIssueBase
{
    [HiddenInput]
    public int Id { get; set; }
}