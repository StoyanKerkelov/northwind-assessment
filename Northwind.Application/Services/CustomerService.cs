using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using Northwind.Application.Dtos;
using Northwind.Application.Interfaces;
using Northwind.Application.Models;

namespace Northwind.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly ILogger<CustomerService> _logger;
        private readonly IMemoryCache _cache;

        public CustomerService(ICustomerRepository repository, ILogger<CustomerService> logger, IMemoryCache cache)
        {
            _repository = repository;
            _logger = logger;
            _cache = cache;
        }

        public async Task<PagedResult<CustomerSummaryDto>> GetCustomersAsync(string? search, int page, int pageSize, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving customers with search term {Search}", search);

            page = Math.Max(page, 1);

            pageSize = Math.Clamp(pageSize, 1, 100);

            var cacheKey = $"customers:{search}:{page}:{pageSize}";

            if (_cache.TryGetValue(cacheKey, out PagedResult<CustomerSummaryDto>? cached))
            {
                _logger.LogInformation("Cache hit for {CacheKey}", cacheKey);

                return cached!;
            }

            _logger.LogInformation("Cache miss for {CacheKey}", cacheKey);

            var result = await _repository.GetCustomersAsync(search, page, pageSize, cancellationToken);

            _cache.Set(cacheKey, result, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow =
                TimeSpan.FromMinutes(5)
            });

            return result;
        }

        public async Task<CustomerDetailDto?> GetCustomerByIdAsync(string customerId, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving customer {CustomerId}", customerId);

            var customer = await _repository.GetCustomerByIdAsync(customerId, cancellationToken);

            if (customer is null)
            {
                _logger.LogWarning("Customer {CustomerId} was not found", customerId);
            }

            return customer;
        }
    }
}