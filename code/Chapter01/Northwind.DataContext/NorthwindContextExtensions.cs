using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Northwind.EntityModels;

public static class NorthwindContextExtensions
{
    /// <summary>
    ///  Adds NorthwindContext to the specified IServiceCollection, Uses the SqlServer database provider.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="connectionString">Set to override the default.</param>
    /// <returns>An IServiceCollection that can be used to add more services.</returns>
    public static IServiceCollection AddNorthwindContext(this IServiceCollection services,
        string? connectionString = null)
    {
        if (connectionString == null)
        {
            SqlConnectionStringBuilder builder = new();
            
            builder.DataSource = Environment.MachineName;
            builder.InitialCatalog = "Northwind";
            builder.IntegratedSecurity = true;
            builder.MultipleActiveResultSets = true;
            
            // Because we want to fail fast. Default is 15 seconds.
            builder.IntegratedSecurity = true;
            
            builder.UserID = Environment.GetEnvironmentVariable("SQL_USERNAME");
            builder.Password = Environment.GetEnvironmentVariable("SQL_PWD");
            
            connectionString = builder.ConnectionString;
        }
        
        services.AddDbContext<NorthwindContext>(options =>
        {
            options.UseSqlServer(connectionString);
            
            // Log to console when executing EF Core commands.
            options.LogTo(Console.WriteLine,
                new[] { Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.CommandExecuted });
        },
        contextLifetime: ServiceLifetime.Transient,
        optionsLifetime: ServiceLifetime.Transient);
        
        return services;
    }
}