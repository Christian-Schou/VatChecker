using Microsoft.OpenApi.Models;
using System.Reflection;
using VatChecker.Interfaces;
using VatChecker.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console());

Log.Information("Registering all required services for running the API...");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
Log.Information("Controllers and endpoint exploration has been added...");

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "API VAT Checker",
            Version = "v1",
            Description = "This API can be used to validate VAT/TAX Numbers using the VIES database. Complete docs can be found at '/api-docs'.",
            Contact = new OpenApiContact
            {
                Name = "Christian Schou",
                Email = "chsc@christian-schou.com"
            }
        });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
    options.EnableAnnotations();
});
Log.Information("Swagger API Documentation was added...");

// Register EU Vat Checker Service
builder.Services.AddTransient<IEUVatChecker, EUVatChecker>();
Log.Information("EU Vat Services has been registered in API...");

var app = builder.Build();
Log.Information("API is booting up, please wait...");
// Configure the HTTP request pipeline.

Log.Information("Using the registered services for the API...");
Log.Information("Using Swagger API Documentation...");
app.UseSwagger();
app.UseSwaggerUI(options =>
    options.SwaggerEndpoint("/swagger/v1/swagger.json",
    "API VAT Checker v1"));

Log.Information("Using ReDoc API Documentation...");
app.UseReDoc(options =>
{
    options.DocumentTitle = "API VAT Checker";
    options.SpecUrl = "/swagger/v1/swagger.json";
});

app.UseAuthorization();

app.MapControllers();
Log.Information("VAT API is is READY!");

app.Run();
