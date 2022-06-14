﻿using System.Text;
using System.Text.Json;
using VShop.Web.Models;
using VShop.Web.Services.Contracts;

namespace VShop.Web.Services
{
    public class ProductService : IProductService

    {
        private readonly IHttpClientFactory clientFactory;
        private const string apiEndpoint = "/api/products/";
        private readonly JsonSerializerOptions options;
        private ProductViewModel productVM;
        private IEnumerable<ProductViewModel> productsVM;

        public ProductService(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
            options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public async Task<IEnumerable<ProductViewModel>> GetAllProducts()
        {
            var client = clientFactory.CreateClient("ProductApi");

            using (var response = await client.GetAsync(apiEndpoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    productsVM = await JsonSerializer
                                .DeserializeAsync<IEnumerable<ProductViewModel>>(apiResponse, options);
                }
                else
                {
                    return null;
                }
            }
            return productsVM;
        }

        public async Task<ProductViewModel> FindProductById(int id)
        {
            var client = clientFactory.CreateClient("ProductApi");

            using (var response = await client.GetAsync(apiEndpoint + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    productVM = await JsonSerializer
                                .DeserializeAsync<ProductViewModel>(apiResponse, options);
                }
                else
                {
                    return null;
                }
            }
            return productVM;
        }

        public async Task<ProductViewModel> CreateProduct(ProductViewModel productVM)
        {
            var client = clientFactory.CreateClient("ProductApi");

            StringContent content = new StringContent(JsonSerializer.Serialize(productVM),
                                    Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync(apiEndpoint, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    productVM = await JsonSerializer
                                .DeserializeAsync<ProductViewModel>(apiResponse, options);
                }
                else
                {
                    return null;
                }
            }
            return productVM;
        }

        public async Task<ProductViewModel> UpdateProduct(ProductViewModel productVM)
        {
            var client = clientFactory.CreateClient("ProductApi");
            ProductViewModel productUpdated = new ProductViewModel();

            using (var response = await client.PutAsJsonAsync(apiEndpoint, productVM))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    productUpdated = await JsonSerializer
                                .DeserializeAsync<ProductViewModel>(apiResponse, options);
                }
                else
                {
                    return null;
                }
            }
            return productUpdated;
        }

        public async Task<bool> DeleteProductById(int id)
        {
            var client = clientFactory.CreateClient("ProductApi");

            using (var response = await client.DeleteAsync(apiEndpoint + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
