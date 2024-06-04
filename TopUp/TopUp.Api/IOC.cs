﻿using TopUp.Application.Services;
using TopUp.Domain.Interfaces;
using TopUp.Infrastructure.Repositories;

namespace TopUp.Api
{
    public static class IOC
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IBeneficiaryService, BeneficiaryService>();

            return services;
        }
    }
}
