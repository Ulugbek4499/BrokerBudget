using BrokerBudget.Application.Common.Interfaces;
using BrokerBudget.Domain.Entities.Identity;
using BrokerBudget.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace BrokerBudget.API
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IApplicationUser, CurrentUser>();
            services.AddControllers().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddEndpointsApiExplorer();
            services.AddHttpContextAccessor();
            services.AddSwaggerGen();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Example API", Version = "v1" });

            });
            return services;
        }
    }
}
