using AutoMapper;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<Comment, CreateCommentCommand>();
        CreateMap<CreateCommentCommand, Comment>();
    }
}