using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public DbSet<Issue> Issues { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<IssueTag> IssueTags { get; set; }
    public DbSet<Project> Projects { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public sealed override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return this.SaveChangesAsync(acceptAllChangesOnSuccess: true, cancellationToken);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        foreach (var changedEntity in ChangeTracker.Entries())
        {
            if (changedEntity.Entity is IAuditable entity)
            {
                switch (changedEntity.State)
                {
                    case EntityState.Added:
                        entity.CreatedDate = now;
                        entity.UpdatedDate = now;
                        break;
                    case EntityState.Modified:
                        Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                        entity.UpdatedDate = now;
                        break;
                }
            }
        }

        return await base.SaveChangesAsync(true, cancellationToken).ConfigureAwait(false);
    }

    public override int SaveChanges()
    {
        var now = DateTime.UtcNow;

        foreach (var changedEntity in ChangeTracker.Entries())
        {
            if (changedEntity.Entity is IAuditable entity)
            {
                switch (changedEntity.State)
                {
                    case EntityState.Added:
                        entity.CreatedDate = now;
                        entity.UpdatedDate = now;
                        break;
                    case EntityState.Modified:
                        Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                        entity.UpdatedDate = now;
                        break;
                }
            }
        }

        return base.SaveChanges();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        GlobalQueryFilterService.ApplyGlobalQueryFilters(builder);

        builder
            .Entity<Issue>()
            .HasMany(p => p.Assignees)
            .WithMany(p => p.AssignedIssues);

        builder
            .Entity<User>()
            .HasMany(p => p.AuthoredIssues)
            .WithOne(p => p.Author);

        int issueTagSeedId = -100;
        builder
            .Entity<IssueTag>()
            .HasData(
                new IssueTag
                {
                    Id = issueTagSeedId--,
                    Slug = "bug",
                    Label = "bug",
                    Description = "Indicates an unexpected problem or unintended behavior"
                },
                new IssueTag
                {
                    Id = issueTagSeedId--,
                    Slug = "documentation",
                    Label = "documentation",
                    Description = "Indicates a need for improvements or additions to documentation"
                },
                new IssueTag
                {
                    Id = issueTagSeedId--,
                    Slug = "duplicate",
                    Label = "duplicate",
                    Description = "Indicates similar issues, pull requests, or discussions"
                },
                new IssueTag
                {
                    Id = issueTagSeedId--,
                    Slug = "enhancement",
                    Label = "enhancement",
                    Description = "Indicates new feature requests"
                },
                new IssueTag
                {
                    Id = issueTagSeedId--,
                    Slug = "good-first-issue",
                    Label = "good first issue",
                    Description = "Indicates a good issue for first-time contributors"
                },
                new IssueTag
                {
                    Id = issueTagSeedId--,
                    Slug = "help-wanted",
                    Label = "help wanted",
                    Description = "Indicates that a maintainer wants help on an issue or pull request"
                },
                new IssueTag
                {
                    Id = issueTagSeedId--,
                    Slug = "invalid",
                    Label = "invalid",
                    Description = "Indicates that an issue, pull request, or discussion is no longer relevant"
                },
                new IssueTag
                {
                    Id = issueTagSeedId--,
                    Slug = "question",
                    Label = "question",
                    Description = "Indicates that an issue, pull request, or discussion needs more information"
                },
                new IssueTag
                {
                    Id = issueTagSeedId--,
                    Slug = "wontfix",
                    Label = "wontfix",
                    Description = "Indicates that work won't continue on an issue, pull request, or discussion"
                }
            );
    }
}