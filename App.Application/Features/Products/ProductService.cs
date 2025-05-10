using App.Application.Contracts.Caching;
using App.Application.Contracts.Persistence;
using App.Application.Contracts.ServiceBus;
using App.Application.Features.Products.Create;
using App.Application.Features.Products.Dto;
using App.Application.Features.Products.Update;
using App.Application.Features.Products.UpdateStock;
using App.Domain.Entities;
using App.Domain.Events;
using AutoMapper;
using System.Net;

namespace App.Application.Features.Products;

    public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService, IServiceBus serviceBus) : IProductService
    {
        private const string ProductListCacheKey = "ProductList";

        public async Task<ServiceResult<List<ProductDto>>> GetAllProductsAsync()
        {
            var cachedProductList = await cacheService.GetAsync<List<ProductDto>>(ProductListCacheKey);

            if (cachedProductList is not null) return ServiceResult<List<ProductDto>>.Success(cachedProductList);

            var products = await productRepository.GetAllAsync();
            var productsAsDto = mapper.Map<List<ProductDto>>(products);

            await cacheService.AddAsync(ProductListCacheKey, productsAsDto, TimeSpan.FromMinutes(5));

            return ServiceResult<List<ProductDto>>.Success(productsAsDto);
        }

        public async Task<ServiceResult<List<ProductDto>>> GetPaginatedAllProductsAsync(int pageNum, int pageSize)
        {
            var products = await productRepository.GetAllPagedAsync(pageNum, pageSize);
            var productsAsDto = mapper.Map<List<ProductDto>>(products);
            
            return ServiceResult<List<ProductDto>>.Success(productsAsDto);
        }

        public async Task<ServiceResult<List<ProductDto>>> GetTopPricedAsync(int count)
        {
            var products = await productRepository.GetTopPricedProductsAsync(count);

            var productsAsDto = mapper.Map<List<ProductDto>>(products);

            return new ServiceResult<List<ProductDto>>()
            {
                Data = productsAsDto
            };
        }

        public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product is null)
            {
                return ServiceResult<ProductDto?>.Failure($"Product with id {id} not found", HttpStatusCode.NotFound);
            }

            var productAsDto = mapper.Map<ProductDto>(product);

            return ServiceResult<ProductDto?>.Success(productAsDto!);
        }

        public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
        {
            var anyExistingProduct = await productRepository
                .AnyAsync(p => p.Name == request.Name);

            if (anyExistingProduct)
            {
                return ServiceResult<CreateProductResponse>
                    .Failure($"Product with name {request.Name} already exists", HttpStatusCode.Conflict);
            }

            var product=mapper.Map<Product>(request);

            await productRepository.AddAsync(product);
            await unitOfWork.SaveChangesAsync();
            
            await serviceBus.PublishAsync(new ProductAddedEvent(product.Id, product.Name, product.Price));
            await cacheService.RemoveAsync(ProductListCacheKey);

            return ServiceResult<CreateProductResponse>
                .SuccessAsCreated(new CreateProductResponse(product.Id), $"api/products/{product.Id}");
        }

        public async Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request)
        {
            var anyExistingProduct = await productRepository
                .AnyAsync(p => p.Name == request.Name && !p.Id.Equals(id));

            if (anyExistingProduct)
            {
                return ServiceResult.Failure($"Product with name {request.Name} already exists", HttpStatusCode.Conflict);
            }

            var product = mapper.Map<Product>(request);
            product.Id = id;
            productRepository.Update(product);

            await unitOfWork.SaveChangesAsync();
            await cacheService.RemoveAsync(ProductListCacheKey);

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request)
        {
            var product = await productRepository.GetByIdAsync(request.ProductId);
            if (product is null)
            {
                return ServiceResult.Failure($"Product with id {request.ProductId} not found", HttpStatusCode.NotFound);
            }

            product.Stock = request.Quantity;
            productRepository.Update(product);
            await unitOfWork.SaveChangesAsync();
            await cacheService.RemoveAsync(ProductListCacheKey);

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            productRepository.Delete(product!);
            await unitOfWork.SaveChangesAsync();
            await cacheService.RemoveAsync(ProductListCacheKey);

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
    }
