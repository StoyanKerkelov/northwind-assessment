namespace Northwind.Infrastructure.Models
{
    public class CustomerDetailDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? ContactName { get; set; }
        public string? Country { get; set; }
        public List<OrderSummaryDto> Orders { get; set; } = [];
    }
}