using System.ComponentModel.DataAnnotations;

public class Project : EntityBase<int>
{
    [RegularExpression(@"^([\w\d]+-?)+$", ErrorMessage = "Name cannot contain spaces and can only be seperated by hyphen (-)")]
    public string Name { get; set; }
    public string? Description { get; set; }
    public List<Issue> Issues { get; set; } = new List<Issue>();
}