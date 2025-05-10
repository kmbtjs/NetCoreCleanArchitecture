using App.Application.Contracts.Persistence;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Products
{
    public class ProductRepository(AppDbContext dbContext) : GenericRepository<Product, int>(dbContext), IProductRepository
    {
        public Task<List<Product>> GetTopPricedProductsAsync(int count)
        {
            return DbContext.Products
                .OrderByDescending(p => p.Price)
                .Take(count)
                .ToListAsync();
        }
    }
}
