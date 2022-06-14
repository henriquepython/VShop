using System.Text.Json;
using VShop.Web.Models;
using VShop.Web.Services.Contracts;

namespace VShop.Web.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly JsonSerializerOptions options;
        private const string apiEndpoint = "/api/categories/";

        public CategoryService(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
            options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public async Task<IEnumerable<CategoryViewModel>> GetAllCategories()
        {
            var client = clientFactory.CreateClient("ProductApi");

            IEnumerable<CategoryViewModel> categories;

            var response = await client.GetAsync(apiEndpoint);

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();

                categories = await JsonSerializer
                            .DeserializeAsync<IEnumerable<CategoryViewModel>>(apiResponse, options);
            }
            else
            {
                return null;
            }
            return categories;
        }
    }
}
