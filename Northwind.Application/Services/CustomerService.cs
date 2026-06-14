using Microsoft.Extensions.Logging;
using Northwind.Application.Dtos;
using Northwind.Application.Interfaces;
using NorthwindNorthwind.Application.Interfaces;

namespace Northwind.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly ILogger<CustomerService> _logger;
        public CustomerService(ICustomerRepository repository, ILogger<CustomerService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<CustomerSummaryDto>> GetCustomersAsync(string? search, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving customers with search term {Search}", search);

            return await _repository.GetCustomersAsync(search, cancellationToken);
        }

        public async Task<CustomerDetailDto?> GetCustomerByIdAsync(string customerId, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving customer {CustomerId}", customerId);


            var customer = await _repository.GetCustomerByIdAsync(customerId, cancellationToken);

            if (customer is null)
            {
                _logger.LogWarning("Customer {CustomerId} was not found", customerId);
            }

            return customer;
        }
    }
}