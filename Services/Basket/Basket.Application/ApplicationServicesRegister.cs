using Basket.Application.Handlers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application
{
    public static class ApplicationServicesRegister
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //Register Mediatr
            var assemblies = new Assembly[]
            {
            Assembly.GetExecutingAssembly(),
            typeof(GetBasketByUserNameHandler).Assembly
            };
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

            return services;
        }
    }
}
