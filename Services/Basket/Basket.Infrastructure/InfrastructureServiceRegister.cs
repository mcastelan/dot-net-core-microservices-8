using AutoMapper;
using Basket.Core.Repositories;
using Basket.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Infrastructure
{
    public static class InfrastructureServiceRegister
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string setting )
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = setting;//builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
            });

            services.AddScoped<IBasketRepository, BasketRepository>();

            return services;
        }
    }
}
