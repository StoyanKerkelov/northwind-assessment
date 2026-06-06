using Dapper;
using Northwind.Infrastructure.Database;
using Northwind.Infrastructure.Models;

namespace Northwind.Infrastructure.Repositories
{

    public class CustomerRepository : ICustomerRepository
    {
        private readonly ConnectionFactory _connectionFactory;

        public CustomerRepository(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<CustomerSummaryDto>> GetCustomersAsync(string? search, CancellationToken cancellationToken)
        {
            using var connection = _connectionFactory.CreateConnection();

            const string sql = """
            SELECT
                c.CustomerID AS Id,
                c.CompanyName AS Name,
                COUNT(o.OrderID) AS OrderCount
            FROM Customers c
            LEFT JOIN Orders o
                ON c.CustomerID = o.CustomerID
            WHERE
                @Search IS NULL
                OR c.CompanyName LIKE '%' + @Search + '%'
            GROUP BY
                c.CustomerID,
                c.CompanyName
            ORDER BY
                c.CompanyName
            """;

            return await connection.QueryAsync<CustomerSummaryDto>(sql, new { Search = search });
        }
    }
}
