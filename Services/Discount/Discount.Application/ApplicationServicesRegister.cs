


using Discount.Application.Handlers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Discount.Application;

public static class ApplicationServicesRegister
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        
        //Register Mediatr
        var assemblies = new Assembly[]
        {
            Assembly.GetExecutingAssembly(),
            typeof(GetDiscountQueryHandler).Assembly
        };
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

        return services;
    }
}
