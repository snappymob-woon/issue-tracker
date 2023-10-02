using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

public static class GlobalQueryFilterService
{
    public static void ApplyGlobalQueryFilters(ModelBuilder modelBuilder)
    {
        foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
        {
            // Check if the entity type has a property named "IsDeleted" of type boolean.
            var isDeletedProperty = entityType.FindProperty("IsDeleted");

            if (isDeletedProperty != null && isDeletedProperty.ClrType == typeof(bool))
            {
                // Apply the global query filter to the entity type.
                var parameter = Expression.Parameter(entityType.ClrType, "x");
                var body = Expression.Equal(
                    Expression.Property(parameter, isDeletedProperty.PropertyInfo),
                    Expression.Constant(false)
                );

                var lambda = Expression.Lambda(body, parameter);
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }
    }
}