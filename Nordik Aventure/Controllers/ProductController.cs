using GestBibli.Objects.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Nordik_Aventure.Objects.Models;
using Nordik_Aventure.Services;

namespace Nordik_Aventure.Controllers;

[Route("stock/products")]
public class ProductController : Controller
{
    private ProductService _productService;
    private SupplierService _supplierService;

    public ProductController(ProductService productService, SupplierService supplierService)
    {
        _productService = productService;
        _supplierService = supplierService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var result = _productService.GetAllProducts();
        return View("../ModuleStock/AllProducts", result.Data);
    }

    [HttpGet]
    [Route("/add")]
    public IActionResult AddProductForm()
    {
        var categories = _productService.GetAllCategories();
        var suppliers = _supplierService.GetAllSuppliers();
        var productVM = new AddProductViewModel()
        {
            Categories = categories.Data,
            Suppliers = suppliers.Data,
        };
        return View("../ModuleStock/AddProduct", productVM);
    }

    [HttpPost]
    public IActionResult AddProduct([FromForm] AddProductViewModel productVM)
    {
        var product = new Product()
        {
            Name = productVM.Name,
            Sku = productVM.Sku,
            PriceToBuy = productVM.PriceToBuy,
            PriceToSell = productVM.PriceToSell,
            PaybackToSupplier = productVM.PaybackToSupplier,
            Weight = productVM.Weight,
            Status = productVM.Status,
            SupplierId = productVM.SelectedSupplierId,
            CategoryId = productVM.SelectedCategoryId,
            GrossMargin = ((productVM.PriceToSell - productVM.PriceToBuy) / productVM.PriceToSell) * 100,
        };
        var result = _productService.CreateProduct(product);
        if (!result.Success)
        {
            TempData["ErrorMessage"] = result.Message;
            TempData["ErrorType"] = "error";
            return RedirectToAction("AddProductForm");
        }

        TempData["ErrorType"] = "success";
        TempData["ErrorMessage"] = "Produit ajouté avec succès";
        return RedirectToAction("Index");
        
    }
}