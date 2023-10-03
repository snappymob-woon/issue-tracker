using System.ComponentModel.DataAnnotations;

public class EditProjectBase
{
    [RegularExpression(@"^([\w\d]+-?)+$", ErrorMessage = "Name cannot contain spaces and can only be seperated by hyphen (-)")]
    public string Name { get; set; }
    public string? Description { get; set; }
}