using Microsoft.AspNetCore.Mvc.RazorPages;
using Northwind.Application.Dtos;
using Northwind.Application.Models;

namespace Northwind.Web.Pages
{
    public class CustomersModel : PageModel
    {
        private readonly IHttpClientFactory _factory;
        public PagedResult<CustomerSummaryDto> Customers { get; set; } = new();

        public CustomersModel(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task OnGetAsync()
        {
            var client = _factory.CreateClient("NorthwindApi");

            Customers =  await client.GetFromJsonAsync<PagedResult<CustomerSummaryDto>>( "api/customers");
        }
    }
}