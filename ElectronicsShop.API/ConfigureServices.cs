using ElectronicsShop.API.Filters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ElectronicsShop.API;

public static class ConfigureServices
{
    public static IServiceCollection AddAPIServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();

        var hosts = configuration.GetSection("Cors:AllowedOrigins").Get<List<string>>();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowedOrigins", p =>
            {
                p.AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins(hosts.ToArray());
            });
        });


        services.AddControllers(options => {
            options.Filters.Add<ApiExceptionFilterAttribute>();
            options.Filters.Add<UnifyResponseFilter>();
        }).AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
        .AddFluentValidation(options =>
            options.AutomaticValidationEnabled = false);

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddOpenApiDocument(config =>
        {
            config.Title= "ElectronicsShop API";
            config.Version = "v1";
            config.DocumentName = "ElectronicsShopAPI_v1";
            config.GenerateEnumMappingDescription = true;
        });
        
        return services;
    }
}