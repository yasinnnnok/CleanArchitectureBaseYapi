﻿
using CleanArchitecture.Application.Behaviors;
using FluentValidation;
using MediatR;

namespace CleanArchitecture.WebApi.Configurations;

public sealed class ApplicationServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration,IHostBuilder host)
    {
        //mediaTr servis registerion u yapıldı
        services.AddMediatR(cfr => cfr.RegisterServicesFromAssembly(typeof(CleanArchitecture.Application.AssemblyReference).Assembly));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaivor<,>));
        services.AddValidatorsFromAssembly(typeof(CleanArchitecture.Application.AssemblyReference).Assembly);
    }
}
