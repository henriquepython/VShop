using AutoMapper;
using VShop.ProductApi.Dtos;
using VShop.ProductApi.Models;
using VShop.ProductApi.Repositories;

namespace VShop.ProductApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var productEntity = await productRepository.GetAll();
            return mapper.Map<IEnumerable<ProductDto>>(productEntity);
        }
        public async Task<ProductDto> GetProductById(int id)
        {
            var productEntity = await productRepository.GetById(id);
            return mapper.Map<ProductDto>(productEntity);
        }
        public async Task AddProduct(ProductDto productDto)
        {
            var productEntity = mapper.Map<Product>(productDto);
            await productRepository.Create(productEntity);
            productDto.Id = productEntity.Id;
        }
        public async Task UpdateProduct(ProductDto productDto)
        {
            var productEntity = mapper.Map<Product>(productDto);
            await productRepository.Update(productEntity);
        }
        public async Task RemoveProduct(int id)
        {
            var productEntity = productRepository.GetById(id).Result;
            await productRepository.Delete(productEntity.Id);
        }
       
    }
}
