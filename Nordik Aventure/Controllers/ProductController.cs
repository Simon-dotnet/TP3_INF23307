using Microsoft.AspNetCore.Mvc;
using Nordik_Aventure.Services;

namespace Nordik_Aventure.Controllers;

[Route("stock/products")]
public class ProductController : Controller
{
    private ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var result = _productService.GetAllProducts();
        return View("../ModuleStock/AllProducts", result.Data);
    }
}