public class Comment : EntityBase<int>
{
    public User? User { get; set; }
    public string Body { get; set; }
}