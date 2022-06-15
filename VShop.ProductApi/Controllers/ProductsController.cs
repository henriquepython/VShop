using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VShop.ProductApi.Dtos;
using VShop.ProductApi.Services;

namespace VShop.ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get()
        {
            var productsDto = await productService.GetProducts();
            if (productsDto is null)
                return NotFound("Products not found");

            return Ok(productsDto);
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var productDto = await productService.GetProductById(id);
            if (productDto is null)
                return NotFound("Product not found");

            return Ok(productDto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDto productDto)
        {
            if (productDto is null)
                return BadRequest("Invalid Data");

            await productService.AddProduct(productDto);

            return new CreatedAtRouteResult("GetProduct", new { id = productDto.Id },
                productDto);
        }

        [HttpPut()]
        public async Task<ActionResult> Put([FromBody] ProductDto productDto)
        {
           
            if (productDto is null)
                return BadRequest("Data invalid");

            await productService.UpdateProduct(productDto);

            return Ok(productDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProductDto>> Delete(int id)
        {
            var productDto = await productService.GetProductById(id);
            if (productDto is null)
                return NotFound("Product not found");

            await productService.RemoveProduct(id);

            return Ok(productDto);
        }
    }
}
