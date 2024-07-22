using Microsoft.Extensions.Configuration;
using VatChecker.Application.Interfaces;
using VatChecker.Infrastructure.Services.Vies;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(TimeProvider.System);
        services.AddTransient<IViesService, ViesService>();
        
        return services;
    }
}
