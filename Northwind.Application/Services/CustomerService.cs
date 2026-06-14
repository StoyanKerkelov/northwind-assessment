using Microsoft.Extensions.Logging;
using Northwind.Application.Dtos;
using Northwind.Application.Interfaces;
using Northwind.Application.Models;

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

        public async Task<PagedResult<CustomerSummaryDto>> GetCustomersAsync(string? search, int page, int pageSize, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving customers with search term {Search}", search);

            page = Math.Max(page, 1);

            pageSize = Math.Clamp(pageSize, 1, 100);

            return await _repository.GetCustomersAsync(search, page, pageSize, cancellationToken);
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