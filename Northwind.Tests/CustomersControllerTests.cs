using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Northwind.Api.Controllers;
using Northwind.Application.Dtos;
using Northwind.Application.Interfaces;

namespace Northwind.Tests
{
    public class CustomersControllerTests
    {
        [Fact]
        public async Task GetCustomer_ShouldReturnNotFound_WhenCustomerDoesNotExist()
        {
            // Arrange
            var service = new Mock<ICustomerService>();

            service.Setup(r => r.GetCustomerByIdAsync("INVALID", It.IsAny<CancellationToken>()))
                                   .ReturnsAsync((CustomerDetailDto?)null);

            var controller = new CustomersController(service.Object);

            // Act
            var result =  await controller.GetCustomer("INVALID", CancellationToken.None);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetCustomer_ShouldReturnOk_WhenCustomerExists()
        {
            // Arrange
            var service = new Mock<ICustomerService>();

            service.Setup(r => r.GetCustomerByIdAsync("ALFKI", It.IsAny<CancellationToken>()))
                             .ReturnsAsync(new CustomerDetailDto { Id = "ALFKI", Name = "Alfreds Futterkiste" });

            var controller = new CustomersController(service.Object);

            // Act
            var result = await controller.GetCustomer("ALFKI", CancellationToken.None);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}