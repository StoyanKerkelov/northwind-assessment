using Dapper;
using Northwind.Infrastructure.Database;
using Northwind.Infrastructure.Models;

namespace Northwind.Infrastructure.Repositories
{

    public class CustomerRepository : ICustomerRepository
    {
        private readonly ConnectionFactory _connectionFactory;

        #region-sql
        private const string customersSql = """
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


        private const string customerSql = """
                                    SELECT
                                        CustomerID AS Id,
                                        CompanyName AS Name,
                                        ContactName,
                                        Country
                                    FROM Customers
                                    WHERE CustomerID = @CustomerId
                                    """;

        private const string ordersSql = """
                                SELECT
                                    o.OrderID AS OrderId,
                                    o.OrderDate,

                                CAST(SUM(od.UnitPrice * od.Quantity * (1 - od.Discount)) AS DECIMAL(18,2)) AS TotalValue,

                                COUNT(DISTINCT od.ProductID) AS ProductCount

                                FROM Orders o
                                INNER JOIN [Order Details] od
                                    ON o.OrderID = od.OrderID

                                WHERE o.CustomerID = @CustomerId

                                GROUP BY
                                    o.OrderID,
                                    o.OrderDate

                                ORDER BY
                                    o.OrderDate DESC
                                """;
        #endregion-sql

        public CustomerRepository(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<CustomerDetailDto?> GetCustomerByIdAsync(string customerId, CancellationToken cancellationToken)
        {
            using var connection = _connectionFactory.CreateConnection();

            var customer = await connection.QuerySingleOrDefaultAsync<CustomerDetailDto>(customerSql, new { CustomerId = customerId });

            if (customer is null)
                return null;

            var orders = await connection.QueryAsync<OrderSummaryDto>(ordersSql, new { CustomerId = customerId });

            customer.Orders = orders.ToList();

            return customer;
        }

        public async Task<IEnumerable<CustomerSummaryDto>> GetCustomersAsync(string? search, CancellationToken cancellationToken)
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryAsync<CustomerSummaryDto>(customersSql, new { Search = search });
        }
    }
}
