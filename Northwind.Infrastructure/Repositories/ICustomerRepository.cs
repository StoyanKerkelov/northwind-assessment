using Northwind.Infrastructure.Models;

namespace Northwind.Infrastructure.Repositories
{

    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerSummaryDto>> GetCustomersAsync(string? search, CancellationToken cancellationToken);
    }
}
