
namespace Northwind.Infrastructure.Models
{
    public class CustomerSummaryDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int OrderCount { get; set; }
    }
}
