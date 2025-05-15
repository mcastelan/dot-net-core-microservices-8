

using Catalog.Application.Handlers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Catalog.Application;

public static class ApplicationServicesRegister
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //Register Mediatr
        var assemblies = new Assembly[]
        {
            Assembly.GetExecutingAssembly(),
            typeof(GetAllBrandsHandler).Assembly
        };
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

        return services;
    }
}
