using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendIO.Application.Services;


namespace SendIO.Infrastucture
{
	public static class ServiceRegistration
	{
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddTransient<IMinIO, MinIOService>();
            return service;

        }
    }
}

