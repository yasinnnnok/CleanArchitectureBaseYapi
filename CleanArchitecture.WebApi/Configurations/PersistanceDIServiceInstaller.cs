
using CleanArchitecture.Application.Absractions;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Infrastructure.Authentication;
using CleanArchitecture.Persistance.Context;
using CleanArchitecture.Persistance.Repositories;
using CleanArchitecture.Persistance.Services;
using CleanArchitecture.WebApi.Middleware;
using GenericRepository;

namespace CleanArchitecture.WebApi.Configurations;

public sealed class PersistanceDIServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostBuilder host)
    {
        services.AddScoped<ICarService, CarService>();
        services.AddTransient<ExceptionMiddleware>()
            ;
        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRoleServices, RoleService>();
        services.AddScoped<IUserRoleServices, UserRoleService>();

        services.AddScoped<IUnitOfWork>(cfr => cfr.GetRequiredService<AppDbContext>());

       
    }
}
