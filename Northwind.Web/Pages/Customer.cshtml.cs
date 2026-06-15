using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Northwind.Application.Dtos;

namespace Northwind.Web.Pages;

public class CustomerModel : PageModel
{
    private readonly IHttpClientFactory _factory;

    public CustomerDetailDto? Customer { get; private set; }

    public CustomerModel(IHttpClientFactory factory)
    {
        _factory = factory;
    }

    public async Task<IActionResult> OnGetAsync(string id)
    {
        var client = _factory.CreateClient("NorthwindApi");

        Customer = await client.GetFromJsonAsync<CustomerDetailDto>($"api/customers/{id}");

        if (Customer is null)
        {
            return NotFound();
        }

        return Page();
    }
}