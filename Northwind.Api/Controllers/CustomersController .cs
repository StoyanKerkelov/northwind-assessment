using Microsoft.AspNetCore.Mvc;
using Northwind.Infrastructure.Repositories;

namespace Northwind.Api.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _repository;

        public CustomersController(ICustomerRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers([FromQuery] string? search, CancellationToken cancellationToken)
        {
            var customers = await _repository.GetCustomersAsync(search, cancellationToken);

            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(string id, CancellationToken cancellationToken)
        {
            var customer = await _repository.GetCustomerByIdAsync(id, cancellationToken);

            if (customer is null)
                return NotFound();

            return Ok(customer);
        }
    }
}