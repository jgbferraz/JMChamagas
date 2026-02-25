using JMChamagas.Application.Abstractions;
using JMChamagas.Infrastructure.Database;
using JMChamagas.Infrastructure.Persistence;
using JMChamagas.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JMChamagas.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default") ?? "Data Source=jmchamagas.db";

        services.AddDbContext<JMChamagasDbContext>(options =>
        {
            options.UseSqlite(connectionString);
        });

        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IVendaRepository, VendaRepository>();

        services.AddHostedService<DatabaseInitializer>();

        return services;
    }
}
