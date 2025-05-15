using Asp.Versioning;
using Catalog.Application;
using Catalog.Application.Handlers;
using Catalog.Core.Repositories;
using Catalog.Infrastructure;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Repositories;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog", Version = "v1" }); });

//Register AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

////Register Mediatr
///
//var assemblies = new Assembly[]
//{
//    Assembly.GetExecutingAssembly(),
//    typeof(GetAllBrandsHandler).Assembly
//};
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
ApplicationServicesRegister.AddApplicationServices(builder.Services);

//Register Application Services
//builder.Services.AddScoped<ICatalogContext, CatalogContext>();
//builder.Services.AddScoped<IProductRepository, ProductRepository>();
//builder.Services.AddScoped<IBrandRepository, ProductRepository>();
//builder.Services.AddScoped<ITypesRepository, ProductRepository>();
InfrastructureServiceRegister.AddInfrastructureServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
