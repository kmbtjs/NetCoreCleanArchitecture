using App.Repositories.Products;
using App.Services.Filters;
using App.Services.Products;
using App.Services.Products.Create;
using App.Services.Products.Update;
using App.Services.Products.UpdateStock;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{

    public class ProductsController(IProductService product) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllProducts() => CreateActionResult(await product.GetAllProductsAsync());

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public async Task<IActionResult> GetPaginatedAllProducts(int pageNumber, int pageSize) => 
            CreateActionResult(await product.GetPaginatedAllProductsAsync(pageNumber, pageSize));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id) => CreateActionResult(await product.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest request) => CreateActionResult(await product.CreateAsync(request));

        [ServiceFilter(typeof(NotFoundFilter<Product, int>))]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromBody] UpdateProductRequest request, int id) => CreateActionResult(await product.UpdateAsync(id, request));

        [HttpPatch("Stock")]
        public async Task<IActionResult> UpdateStock([FromBody] UpdateProductStockRequest request) => CreateActionResult(await product.UpdateStockAsync(request));

        [ServiceFilter(typeof(NotFoundFilter<Product,int>))]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) => CreateActionResult(await product.DeleteAsync(id));
    }
}
