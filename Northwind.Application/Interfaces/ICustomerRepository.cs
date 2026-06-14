
using Northwind.Application.Dtos;
using Northwind.Application.Models;

namespace Northwind.Application.Interfaces
{

    public interface ICustomerRepository
    {
        Task<PagedResult<CustomerSummaryDto>> GetCustomersAsync(string? search, int page, int pageSize, CancellationToken cancellationToken);
        Task<CustomerDetailDto?> GetCustomerByIdAsync(string customerId, CancellationToken cancellationToken);
    }
}
