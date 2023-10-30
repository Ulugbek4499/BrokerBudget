using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Infrastructure.Services;
using BrokerBudget.Infrastructure.Persistence;
using BrokerBudget.Infrastructure.Persistence.Interceptors;

namespace BrokerBudget.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DbConnect"));
            options.UseLazyLoadingProxies();
        });

        services.AddTransient<IDateTime, DateTimeService>();
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        return services;
    }
}
