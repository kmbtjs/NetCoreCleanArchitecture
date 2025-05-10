using App.Domain.Entities;

namespace App.Application.Contracts.Persistence
{
    public interface IProductRepository : IGenericRepository<Product, int>
    {
        public Task<List<Product>> GetTopPricedProductsAsync(int count);
    }
}
