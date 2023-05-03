using ElectronicsShop.Application.Common.Interfaces;
using ElectronicsShop.Infrastructure.ApplicationContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ElectronicsShop.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionStr = configuration.GetConnectionString("ApplicationDBConnectionString");
        services.AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlServer(connectionStr));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>()!);

        services.AddScoped<ApplicationDbContextInitialiser>();

        return services;
    }
}