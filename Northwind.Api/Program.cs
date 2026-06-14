using Northwind.Api.Middleware;
using Northwind.Application.Interfaces;
using Northwind.Application.Services;
using Northwind.Infrastructure.Database;
using Northwind.Infrastructure.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var connectionString = builder.Configuration.GetConnectionString("Northwind")
                        ?? throw new InvalidOperationException("Connection string 'Northwind' not found.");

builder.Services.AddSingleton(new ConnectionFactory(connectionString));

builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddHealthChecks();

builder.Services.AddMemoryCache(); 

//builder.Services.AddProblemDetails(); // Use ProblemDetails for RFC 7807 compliant error responses.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapHealthChecks("/health");

app.MapControllers();

app.Run();
