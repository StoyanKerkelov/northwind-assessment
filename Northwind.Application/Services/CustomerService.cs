using Northwind.Application.Dtos;
using Northwind.Application.Interfaces;
using NorthwindNorthwind.Application.Interfaces;

namespace Northwind.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CustomerSummaryDto>> GetCustomersAsync(string? search, CancellationToken cancellationToken)
        {
            return await _repository.GetCustomersAsync( search, cancellationToken);
        }

        public async Task<CustomerDetailDto?> GetCustomerByIdAsync(string customerId,  CancellationToken cancellationToken)
        {
            return await _repository.GetCustomerByIdAsync(customerId, cancellationToken);
        }
    }
}
