using Domain.Interfaces;
using Domain.Interfaces.Common;
using Infrastructure.Common;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("WebApiDatabase")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IRoleRepository, RoleRepository>();

        return services;
    }
}