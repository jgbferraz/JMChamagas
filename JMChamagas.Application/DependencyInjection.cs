using JMChamagas.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JMChamagas.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ProdutoAppService>();
        services.AddScoped<VendaAppService>();
        return services;
    }
}
