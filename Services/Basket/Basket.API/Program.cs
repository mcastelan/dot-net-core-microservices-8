using Asp.Versioning;
using Basket.Application;
using Basket.Infrastructure;
using Common.Logging;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//Serilog configuration
builder.Host.UseSerilog(Logging.ConfigureLogger);

// Add API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// Add services to the container.




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { 
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Basket", Version = "v1" }); 
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "Basket", Version = "v2" });

    //Configure swagger to use versioning
    c.DocInclusionPredicate((version, apiDescription) =>
    {
        if (!apiDescription.TryGetMethodInfo(out var methodInfo)) {
            return false;
        }
        var versions = methodInfo.DeclaringType?
        .GetCustomAttributes(true)
        .OfType<ApiVersionAttribute>()
        .SelectMany(a => a.Versions);
        return versions?.Any(v=>$"v{v.ToString()}" ==version)??false;
    });

});

//Register AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);


builder.Services.AddInfrastructureServices(builder.Configuration.GetValue<string>("CacheSettings:ConnectionString"));
builder.Services.AddApplicationServices( builder.Configuration);


// MassTransit configuration
builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ct, cfg) => {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
      
        //cfg.ConfigureEndpoints(ct);
    });
});

//Identity server Changes
//var userPolicy = new AuthorizationPolicyBuilder()
//    .RequireAuthenticatedUser()
//    .Build();

//builder.Services.AddControllers(config => {
//    config.Filters.Add(new AuthorizeFilter(userPolicy));
//});


//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.Authority = "https://localhost:8009"; // Identity Server URL
//        options.Audience = "Basket"; // API resource name
        
//    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json","Basket.API v1");
        c.SwaggerEndpoint("/swagger/v2/swagger.json","Basket.API v2");
    });
}


//app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
