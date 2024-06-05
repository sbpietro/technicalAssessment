using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TopUp.Domain.Interfaces;
using TopUp.Infrastructure.Repositories;

namespace TopUp.Infrastructure
{
    public static class InfraModule
    {
        public static void AddInfraModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContexts(configuration);
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBeneficiaryRepository, BeneficiaryRepository>();
            services.AddScoped<ITopUpTransactionRepository, TopUpTransactionRepository>();
        }

        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");


            if (string.IsNullOrEmpty(connectionString))
                connectionString = configuration.GetValue<string>("CONNECTION_STRING");

            services.AddDbContext<TopUpContext>(options =>
            {
                options.UseSqlServer(connectionString, ConfigureOptions);
            });

            return services;

            static void ConfigureOptions(SqlServerDbContextOptionsBuilder options)
            {
                options.MigrationsAssembly(typeof(InfraModule).Assembly.FullName);
                options.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
            }
        }
    }
}
