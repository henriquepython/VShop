using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VShop.ProductApi.Dtos;
using VShop.ProductApi.Services;

namespace VShop.ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> Get()
        {
            var categoriesDto = await categoryService.GetCategories();
            if(categoriesDto == null)
                return NotFound("Categories not found");

            return Ok(categoriesDto);
        }

        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategoriesProducts()
        {
            var categoriesDto = await categoryService.GetCategoriesProducts();
            if (categoriesDto == null)
                return NotFound("Categories not found");

            return Ok(categoriesDto);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDto>> GetById(int id)
        {
            var categoriesDto = await categoryService.GetCategoryById(id);
            if (categoriesDto == null)
                return NotFound("Category not found");

            return Ok(categoriesDto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryDto categoryDto)
        {
            if (categoryDto == null)
                return BadRequest("Invalid Data");

            await categoryService.AddCategory(categoryDto);

            return new CreatedAtRouteResult("GetCategory", new { id = categoryDto.CategoryId }, 
                categoryDto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CategoryDto categoryDto)
        {
            if (id != categoryDto.CategoryId)
                return BadRequest();

            if (categoryDto is null)
                return BadRequest();

            await categoryService.UpdateCategory(categoryDto);

            return Ok(categoryDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoryDto>> Delete(int id)
        {
            var categoriesDto = await categoryService.GetCategoryById(id);
            if (categoriesDto is null)
                return NotFound("Category not found");

            await categoryService.RemoveCategory(id);

            return Ok(categoriesDto);
        }
    }
}
