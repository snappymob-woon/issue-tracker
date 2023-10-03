using AutoMapper;

public class CommentService : BaseService<CommentService>
{
    public CommentService(AppDbContext context, ILoggerFactory factory, IMapper mapper) : base(context, factory, mapper)
    {
    }

    public async Task<int> CreateComment(CreateCommentCommand cmd)
    {
        _logger.LogInformation($"====== {cmd.IssueId}");
        var issue = await _context.Issues.FindAsync(cmd.IssueId);

        if (issue is null)
        {
            throw new Exception("The issue does not exist");
        }

        _logger.LogInformation($"====== {cmd.AuthorId}");
        var author = await _context.Users.FindAsync(cmd.AuthorId);

        if (author is null)
        {
            throw new Exception("User not found");
        }

        var comment = _mapper.Map<Comment>(cmd);
        comment.User = author;

        issue.Comments.Add(comment);

        await _context.SaveChangesAsync();

        return comment.Id;
    }
}