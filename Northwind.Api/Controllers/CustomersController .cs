using Microsoft.AspNetCore.Mvc;
using Northwind.Application.Interfaces;

namespace Northwind.Api.Controllers
{
    /// <summary>
    /// Initializes a new instance of the CustomersController.
    /// </summary>
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;

        /// <summary>
        /// Initializes a new instance of the CustomersController.
        /// </summary>
        /// <param name="service">
        /// ICustomerService service.
        /// </param>

        public CustomersController(ICustomerService service)
        {
            _service = service;
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
        public async Task<IActionResult> GetCustomers([FromQuery] string? search, [FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken cancellationToken = default)
        {
            var customers = await _service.GetCustomersAsync(search, page, pageSize, cancellationToken);

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
            var customer = await _service.GetCustomerByIdAsync(id, cancellationToken);

            if (customer is null)
                return NotFound();

            return Ok(customer);
        }
    }
}