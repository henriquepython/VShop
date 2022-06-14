using VShop.ProductApi.Dtos;

namespace VShop.ProductApi.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<ProductDto> GetProductById(int id);
        Task AddProduct(ProductDto productDto);
        Task UpdateProduct(ProductDto productDto);
        Task RemoveProduct(int id);
    }
}
