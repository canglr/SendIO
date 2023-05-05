using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendIO.Application.Interfaces;
using SendIO.Domain.Entities;
using SendIO.Persistence.Context;
using SendIO.Persistence.Repositories;
using SendIO.Persistence.UnitOfWorks;

namespace SendIO.Persistence
{
	public static class ServiceRegistration
	{

		public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<SendIOContext>(conf =>
			{
				var connStr = configuration["SendIODbConnectionString"].ToString();
				conf.UseSqlServer(connStr, opt =>
				{
					opt.EnableRetryOnFailure();
				});
			});

            services.AddScoped<IFileHeadRepository, FileHeadRepository>();
            services.AddScoped<IFileContentRepository, FileContentRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;
		}

	}
}

