
using Northwind.Application.Dtos;

namespace Northwind.Application.Interfaces
{

    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerSummaryDto>> GetCustomersAsync(string? search, CancellationToken cancellationToken);
        Task<CustomerDetailDto?> GetCustomerByIdAsync(string customerId, CancellationToken cancellationToken);
    }
}
