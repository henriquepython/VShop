using AutoMapper;
using VShop.ProductApi.Dtos;
using VShop.ProductApi.Models;
using VShop.ProductApi.Repositories;

namespace VShop.ProductApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<CategoryDto>> GetCategories()
        {
            var categoriesEntity = await categoryRepository.GetAll();
            return mapper.Map<IEnumerable<CategoryDto>>(categoriesEntity);
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesProducts()
        {
            var categoriesEntity = await categoryRepository.GetCatagoriesProducts();
            return mapper.Map<IEnumerable<CategoryDto>>(categoriesEntity);
        }

        public async Task<CategoryDto> GetCategoryById(int id)
        {
            var categoriesEntity = await categoryRepository.GetById(id);
            return mapper.Map<CategoryDto>(categoriesEntity);
        }

        public async Task AddCategory(CategoryDto categoryDto)
        {
            var categoryEntity = mapper.Map<Category>(categoryDto);
            await categoryRepository.Create(categoryEntity);
            categoryDto.CategoryId = categoryEntity.CategoryId;
        }

        public async Task UpdateCategory(CategoryDto categoryDto)
        {
            var categoryEntity = mapper.Map<Category>(categoryDto);
            await categoryRepository.Update(categoryEntity);
        }

        public async Task RemoveCategory(int id)
        {
            var categoryEntity = categoryRepository.GetById(id).Result;
            await categoryRepository.Delete(categoryEntity.CategoryId);
        }

    }
}
