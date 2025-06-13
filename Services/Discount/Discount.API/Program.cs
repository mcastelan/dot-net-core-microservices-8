
using Common.Logging;
using Discount.API.Services;
using Discount.Application;
using Discount.Infrastructure;
using Discount.Infrastructure.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Serilog configuration
builder.Host.UseSerilog(Logging.ConfigureLogger);

// Add gRPC services to the container.
builder.Services.AddAutoMapper(typeof(Program).Assembly);

ApplicationServicesRegister.AddApplicationServices(builder.Services);
InfrastructureServiceRegister.AddInfrastructureServices(builder.Services);

builder.Services.AddGrpc();



var app = builder.Build();

//Migrate Database
app.MigrateDatabase<Program>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<DiscountService>();
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Communication with grpc endpoints must be made through a grpc client");
    });
});

app.Run();