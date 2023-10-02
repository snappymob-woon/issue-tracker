using AutoMapper;

public class IssueProfile : Profile
{
    public IssueProfile()
    {
        CreateMap<Issue, IssueSummaryViewModel>()
            .ForMember(x => x.AuthorName, ex => ex.MapFrom(y => y.Author == null ? "Unknown" : y.Author.UserName));
        CreateMap<IssueSummaryViewModel, Issue>();
        CreateMap<CreateIssueCommand, Issue>();
        CreateMap<EditIssueCommand, Issue>();
        CreateMap<Issue, EditIssueCommand>()
            .ForMember(x => x.AssigneeIds, ex => ex.MapFrom(y => y.Assignees.Select(z => z.Id.ToString())));
    }
}