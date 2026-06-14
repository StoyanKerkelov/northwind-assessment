using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Northwind.Application.Dtos;
using Northwind.Application.Interfaces;
using Northwind.Application.Models;
using Northwind.Application.Services;

namespace Northwind.Tests
{
    public class CustomerServiceTests
    {

        [Fact]
        public async Task GetCustomersAsync_ShouldClampPaginationParameters()
        {
            // Arrange
            var repository = new Mock<ICustomerRepository>();
            var logger = new Mock<ILogger<CustomerService>>();

            repository.Setup(r => r.GetCustomersAsync(null, 1, 100, It.IsAny<CancellationToken>()))
                                   .ReturnsAsync(new PagedResult<CustomerSummaryDto>());

            var service = new CustomerService(repository.Object, logger.Object);

            // Act
            await service.GetCustomersAsync(
                null,
                0,
                999,
                CancellationToken.None);

            // Assert
            repository.Verify(r => r.GetCustomersAsync(null, 1, 100, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetCustomersAsync_ShouldReturnPagedResult()
        {
            // Arrange
            var repository = new Mock<ICustomerRepository>();
            var logger = new Mock<ILogger<CustomerService>>();

            var expected = new PagedResult<CustomerSummaryDto>
            {
                Page = 2,
                PageSize = 20,
                TotalCount = 91,
                Items = new List<CustomerSummaryDto>()
            };

            repository.Setup(r => r.GetCustomersAsync(null, 2, 20, It.IsAny<CancellationToken>()))
                                   .ReturnsAsync(expected);

            var service = new CustomerService(
                repository.Object,
                logger.Object);

            // Act
            var result = await service.GetCustomersAsync(null, 2, 20, CancellationToken.None);

            // Assert
            result.Page.Should().Be(2);
            result.PageSize.Should().Be(20);
            result.TotalCount.Should().Be(91);
        }
    }
}
