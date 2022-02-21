using Microsoft.OpenApi.Models;
using System.Reflection;
using VatChecker.Interfaces;
using VatChecker.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console());

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "EU Vat Checker",
            Version = "v1",
            Description = "This API is a microservice to validate VAT/TAX Numbers using VIES. Complete docs can be found at '/api-docs'.",
            Contact = new OpenApiContact
            {
                Name = "Christian Schou",
                Email = "someemail@somedomain.com"
            }
        });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
    options.EnableAnnotations();
});

// Register EU Vat Checker Service
builder.Services.AddTransient<IEUVatChecker, EUVatChecker>();

var app = builder.Build();
Log.Information("Vat No Validation Server is booting up...");
// Configure the HTTP request pipeline.

Log.Information("Adding Swagger OpenAPI");
app.UseSwagger();
app.UseSwaggerUI(options =>
    options.SwaggerEndpoint("/swagger/v1/swagger.json",
    "EU Vat Checker v1"));

Log.Information("Adding ReDoc API Documentation");
app.UseReDoc(options =>
{
    options.DocumentTitle = "EU Vat Checker";
    options.SpecUrl = "/swagger/v1/swagger.json";
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
Log.Information("EU Vat Services has been registered");
Log.Information("EU Vat Server is READY");
app.Run();
