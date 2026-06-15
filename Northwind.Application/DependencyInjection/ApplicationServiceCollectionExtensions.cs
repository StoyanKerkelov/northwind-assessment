using Microsoft.Extensions.DependencyInjection;
using Northwind.Application.Interfaces;
using Northwind.Application.Services;

namespace Northwind.Application.DependencyInjection
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();

            return services;
        }
    }
}
