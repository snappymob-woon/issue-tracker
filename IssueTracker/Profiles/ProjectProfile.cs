using AutoMapper;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<Project, CreateProjectCommand>();
        CreateMap<CreateProjectCommand, Project>();
        CreateMap<Project, ProjectSummaryViewModel>();
        CreateMap<ProjectSummaryViewModel, Project>();
    }
}