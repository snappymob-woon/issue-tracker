using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public DbSet<Issue> Issues { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Comment> Comments { get; set; }

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

        builder
            .Entity<Issue>()
            .HasMany(p => p.Assignees)
            .WithMany(p => p.AssignedIssues);

        builder
            .Entity<User>()
            .HasMany(p => p.AuthoredIssues)
            .WithOne(p => p.Author);

    }
}