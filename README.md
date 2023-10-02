# IssueTracker

This project uses .NET 7.

SQLite is used as the database for ease of learning.

Entity Framework Core is used to simplify database actions as well as because it is mainly what is being used for communication with database.

DataAnnotations aren't really used much but can be easily added later.

There is also only a simple application of authentication and authorization.

This project encapsulates the CRUD activities of a normal application.

I also try to incorporate as much topics I read in ASP.Net Core in Action such as Tag Helpers and different types of model binding.

## Getting Started
> Note: I used the dotnet CLI as I developed this with VS Code.
1. Run `dotnet restore` to install required packages
2. Run `dotnet ef database update` to update the database with all migrations
3. Run `dotnet watch` to start hot-reload server.
4. Start playing around.