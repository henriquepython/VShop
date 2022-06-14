using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VShop.Web.Models;
using VShop.Web.Services.Contracts;

namespace VShop.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()
        {
            var result = await productService.GetAllProducts();

            if (result is null)
            {
                return View("Error");
            }
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.CategoryId = new SelectList(await
                categoryService.GetAllCategories(), "CategoryId", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                var result = await productService.CreateProduct(productVM);

                if (result != null)
                    return RedirectToAction(nameof(Index));

            }
            else
            {
                ViewBag.CategoryId = new SelectList(await
                                     categoryService.GetAllCategories(), "CategoryId", "Name");
            }
            return View(productVM);
        }
    }
}
