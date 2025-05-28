using Basket.Application.GrpcService;
using Basket.Application.Handlers;
using Discount.Grpc.Protos;
using Microsoft.Extensions.Configuration;
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
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Register Mediatr
            var assemblies = new Assembly[]
            {
            Assembly.GetExecutingAssembly(),
            typeof(GetBasketByUserNameHandler).Assembly
            };
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

            services.AddScoped<DiscountGrpcService>();
            services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(cfg => cfg.Address = new Uri(configuration["GrpcSettings:DiscountUrl"]));
            return services;
        }
    }
}
