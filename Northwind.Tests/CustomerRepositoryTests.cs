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

        [Fact]
        public async Task GetCustomersAsync_ShouldReturnRequestedPageSize()
        {
            // Act
            var result = await _repository.GetCustomersAsync(null, 1, 20, CancellationToken.None);

            // Assert
            result.Items.Should().HaveCountLessThanOrEqualTo(20);
            result.Page.Should().Be(1);
            result.PageSize.Should().Be(20);
        }

        [Fact]
        public async Task GetCustomersAsync_ShouldReturnDifferentPages()
        {
            var page1 = await _repository.GetCustomersAsync(null, 1, 20, CancellationToken.None);

            var page2 = await _repository.GetCustomersAsync(null, 2,  20, CancellationToken.None);

            page1.Items.Select(c => c.Id)
                       .Should().NotIntersectWith(page2.Items.Select(c => c.Id));
        }

        [Fact]
        public async Task GetCustomersAsync_ShouldFilterBySearch()
        {
            var result = await _repository.GetCustomersAsync("alf", 1, 20, CancellationToken.None);

            result.Items.Should().ContainSingle(c => c.Id == "ALFKI");
        }
    }
}
