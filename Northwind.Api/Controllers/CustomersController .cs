using Microsoft.AspNetCore.Mvc;
using NorthwindNorthwind.Application.Interfaces;

namespace Northwind.Api.Controllers
{
    /// <summary>
    /// Initializes a new instance of the CustomersController.
    /// </summary>
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _repository;

        /// <summary>
        /// Initializes a new instance of the CustomersController.
        /// </summary>
        /// <param name="repository">
        /// Customer repository.
        /// </param>
        public CustomersController(ICustomerRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Returns all customers.
        /// </summary>
        /// <param name="search">
        /// Optional customer name filter.
        /// </param>
        /// <param name="cancellationToken">
        /// Request cancellation token.
        /// </param>
        [HttpGet]
        public async Task<IActionResult> GetCustomers([FromQuery] string? search, CancellationToken cancellationToken)
        {
            var customers = await _repository.GetCustomersAsync(search, cancellationToken);

            return Ok(customers);
        }

        /// <summary>
        /// Returns customer details and order history.
        /// </summary>
        /// <param name="id">
        /// Customer identifier.
        /// </param>
        /// <param name="cancellationToken">
        /// Request cancellation token.
        /// </param>
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