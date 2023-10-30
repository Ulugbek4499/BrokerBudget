using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Domain.Entities.Identity;
using BrokerBudget.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BrokerBudget.MVC.Services;

public static class ConfigureServices
{
    public static IServiceCollection AddWebUIServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IApplicationUser, CurrentUser>();

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();

        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddRazorPages();

        services.AddControllersWithViews();

        return services;
    }
}
