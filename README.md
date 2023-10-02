# IssueTracker

This project uses .NET 7.

SQLite is used as the database for ease of learning.

Entity Framework Core is used to simplify database actions as well as because it is mainly what is being used for communication with database.

I added Automapper by my own volition to easily map and project between classes.

DataAnnotations aren't really used much but can be easily added later.

There is also only a simple application of authentication and authorization.

This project encapsulates the CRUD activities of a normal application.

I also try to incorporate as much topics I read in ASP.Net Core in Action such as Tag Helpers and different types of model binding.

## Getting Started
> Note: I used the dotnet CLI as I developed this with VS Code.
1. Run `dotnet restore` to install required packages
1. Run `dotnet ef database update` to update the database with all migrations
1. Run `dotnet watch` to start hot-reload server.
1. Start playing around.

## Recipes

### Creating and registering a service
1. A service needs to be explicitly be registered before it can be injected and used.
1. Create a service in the `Services` folder.
1. Register it in `Program.cs` with your declared `WebApplicationBuilder`. E.g. `builder.Services.AddScoped<IssueService>();`
> Note: There are multiple `AddX<>()` functions such as `AddScoped<>()`, `AddSingleton<>()` and `AddTransient<>()` that have declares how the service is treated over the lifespan of the application. Please read page 205 of ASP.NET Core in Action Third Edition to know more (or just Google it).

### Adding new Model

1. Create a new model in the `Models` folder.
1. You can choose to inherit the `EntityBase<T>` abstract class to include some auditing and soft delete, it is optional but helpful.
1. Run `dotnet ef migrations add <MigrationNameHere>`, replace the `<MigrationNameHere>` with a descriptive name like `AddedSoftDeleteToIssueEntity`.
1. This should create new migration files under the `Migrations` folder.
1. Go to the `AppDbContext.cs` file and add a new `DbSet<T>`, e.g. `public DbSet<Issue> Issues { get; set; }`
1. Feel free to create respective services and whatnot.

```c#
// Models/Issue.cs
public class Issue : EntityBase<int>
{
    // ...fields here
}

// EntityFramework/AppDbContext.cs
public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public DbSet<Issue> Issues { get; set; }
    

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }
    // ... rest of DbSets and overrides
}
```

### Configuring Automapper

1. Create a new `Profile` class that extends the `Profile` base class from Automapper. E.g. `public class IssueProfile : Profile {}`
1. In the constructor of the new `Profile` class, add your mappings. E.g. `CreateMap<Issue, IssueSummaryViewModel>();`
1. Automapper can be configured more deeply, to see [here](https://code-maze.com/automapper-net-core/)

```c#
// Profiles/IssueProfile.cs
public class IssueProfile : Profile
{
    public IssueProfile()
    {
        CreateMap<Issue, IssueSummaryViewModel>();
    }
}
```

### Using Automapper
1. To use Automapper, simply inject it into your class or handler via constructor injection or parameter injection.
1. Add your mapping profiles according to the recipe above, as without it, Automapper will throw an error.
1. View code below to see usage:

```c#
// Issue.cshtml.cs
public class IssuePageModel : PageModel
{
    private readonly IMapper _mapper;
    private readonly IssueService _issueService;

    // To be used in Razor view
    public IssueSummaryViewModel issueSummary;

    public IssuePageModel(IMapper mapper, IssueService issueService) 
    {
        _mapper = mapper;
        _issueService = issueService;
    }

    public async Task OnGet(int id)
    {
        var issue = await _issueService.GetIssue(id);

        // Object successfully mapped to target class.
        issueSummary = _mapper.Map<IssueSummaryViewModel>(issue);
    }
}
```