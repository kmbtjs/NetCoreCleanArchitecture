﻿using App.Application.Features.Products.Create;
using App.Application.Features.Products.Dto;
using App.Application.Features.Products.Update;
using App.Application.Features.Products.UpdateStock;

namespace App.Application.Features.Products;

    public interface IProductService
    {
        Task<ServiceResult<List<ProductDto>>> GetTopPricedAsync(int count);
        Task<ServiceResult<List<ProductDto>>> GetAllProductsAsync();
        Task<ServiceResult<List<ProductDto>>> GetPaginatedAllProductsAsync(int pageNum, int pageSize);
        Task<ServiceResult<ProductDto?>> GetByIdAsync(int id);
        Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request);
        Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request);
        Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request);
        Task<ServiceResult> DeleteAsync(int id);
    }