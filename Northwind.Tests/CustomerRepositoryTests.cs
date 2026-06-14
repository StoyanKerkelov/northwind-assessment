using Northwind.Infrastructure.Database;
using Northwind.Infrastructure.Repositories;
using FluentAssertions;

namespace Northwind.Tests
{
    public class CustomerRepositoryTests
    {
        private readonly CustomerRepository _repository;

        public CustomerRepositoryTests()
        {
            var connectionFactory = new ConnectionFactory(@"Server=localhost;Database=Northwind;Trusted_Connection=True;TrustServerCertificate=True;");

            _repository = new CustomerRepository(connectionFactory);
        }

        [Fact]
        public async Task GetCustomerByIdAsync_ShouldReturnCustomer_WhenCustomerExists()
        {
            // Act
            var result = await _repository.GetCustomerByIdAsync("ALFKI", CancellationToken.None);

            // Assert
            result.Should().NotBeNull();

            result!.Id.Should().Be("ALFKI");

            result.Name.Should().Be("Alfreds Futterkiste");

            result.Orders.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetCustomerByIdAsync_ShouldReturnNull_WhenCustomerDoesNotExist()
        {
            // Act
            var result = await _repository.GetCustomerByIdAsync("INVALID", CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }
    }
}
