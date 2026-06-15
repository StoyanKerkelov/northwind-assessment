using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Application.Interfaces;
using Northwind.Infrastructure.Database;
using Northwind.Infrastructure.Repositories;

namespace Northwind.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Northwind")
                                   ?? throw new InvalidOperationException("Connection string not found.");

            services.AddSingleton(new ConnectionFactory(connectionString));

            services.AddScoped<ICustomerRepository, CustomerRepository>();

            return services;
        }
    }
}
