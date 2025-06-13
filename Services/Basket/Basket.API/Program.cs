using Asp.Versioning;
using Basket.Application;
using Basket.Infrastructure;
using Common.Logging;
using MassTransit;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});


// Add services to the container.

//Serilog configuration
builder.Host.UseSerilog(Logging.ConfigureLogger);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Basket", Version = "v1" }); });

//Register AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);


builder.Services.AddInfrastructureServices(builder.Configuration.GetValue<string>("CacheSettings:ConnectionString"));
builder.Services.AddApplicationServices( builder.Configuration);
builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ct, cfg) => {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
      
        //cfg.ConfigureEndpoints(ct);
    });
});
//builder.Services.AddMassTransitHostedService();

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
