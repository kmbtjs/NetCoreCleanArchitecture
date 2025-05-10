using App.Application.Features.Categories;
using App.Application.Features.Categories.Create;
using App.Application.Features.Categories.Update;
using App.Domain.Entities;
using App.WebAPI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace App.WebAPI.Controllers
{
    public class CategoriesController(ICategoryService categoryService) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllCategories() => CreateActionResult(await categoryService.GetAllListAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategory(int id) => CreateActionResult(await categoryService.GetByIdAsync(id));

        [HttpGet("products")]
        public async Task<IActionResult> GetCategoryWithProducts() => CreateActionResult(await categoryService.GetCategoryWithProductsAsync());

        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetCategoryWithProducts(int id) => CreateActionResult(await categoryService.GetCategoryWithProductsAsync(id));

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request) => CreateActionResult(await categoryService.CreateAsync(request));

        [ServiceFilter(typeof(NotFoundFilter<Category, int>))]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryRequest request, int id) => CreateActionResult(await categoryService.UpdateAsync(id, request));

        [ServiceFilter(typeof(NotFoundFilter<Category, int>))]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id) => CreateActionResult(await categoryService.DeleteAsync(id));


    }
}
