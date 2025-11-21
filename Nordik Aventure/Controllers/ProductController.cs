using GestBibli.Objects.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Nordik_Aventure.Objects.Models;
using Nordik_Aventure.Services;

namespace Nordik_Aventure.Controllers;

[Route("stock/products")]
public class ProductController : Controller
{
    private readonly ProductService _productService;
    private readonly SupplierService _supplierService;

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
    [Route("add")]
    public IActionResult AddProductForm()
    {
        var categories = _productService.GetAllCategories();
        var suppliers = _supplierService.GetAllSuppliers();
        var productVM = new ProductViewModel()
        {
            Categories = categories.Data,
            Suppliers = suppliers.Data,
        };
        return View("../ModuleStock/AddProduct", productVM);
    }

    [HttpPost]
    public IActionResult AddProduct([FromForm] ProductViewModel productVM)
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
            GrossMargin = Math.Round((productVM.PriceToSell - productVM.PriceToBuy) / productVM.PriceToSell * 100, 2),
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

    [HttpGet]
    [Route("edit/{productId}")]
    public IActionResult ModifyProductForm(int productId)
    {
        var categories = _productService.GetAllCategories();
        var suppliers = _supplierService.GetAllSuppliers();
        var product = _productService.GetProductById(productId).Data;
        var productVM = new ProductViewModel()
        {
            Id = productId,
            Name = product.Name,
            Sku = product.Sku,
            PriceToBuy = product.PriceToBuy,
            PriceToSell = product.PriceToSell,
            PaybackToSupplier = product.PaybackToSupplier,
            Weight = product.Weight,
            Status = product.Status,
            SelectedCategoryId = product.CategoryId,
            SelectedSupplierId = product.SupplierId,
            Categories = categories.Data,
            Suppliers = suppliers.Data,
        };
        return View("../ModuleStock/ModifyProduct", productVM);
    }

    [HttpPost]
    [Route("edit")]
    public IActionResult ModifyProduct([FromForm] ProductViewModel productVM)
    {
        var existingProduct = _productService.GetProductById(productVM.Id).Data;
        existingProduct.Name = productVM.Name;
        existingProduct.PriceToSell = productVM.PriceToSell;
        existingProduct.PriceToBuy = productVM.PriceToBuy;
        existingProduct.Weight = productVM.Weight;
        existingProduct.Status = productVM.Status;
        existingProduct.CategoryId = productVM.SelectedCategoryId;
        existingProduct.SupplierId = productVM.SelectedSupplierId;
        existingProduct.PaybackToSupplier = productVM.PaybackToSupplier;
        existingProduct.GrossMargin =
            Math.Round((productVM.PriceToSell - productVM.PriceToBuy) / productVM.PriceToSell * 100, 2);
        var result = _productService.UpdateProduct(existingProduct);
        if (!result.Success)
        {
            TempData["ErrorMessage"] = result.Message;
            TempData["ErrorType"] = "error";
            return RedirectToAction("ModifyProductForm", new { productId = existingProduct.Id });
        }

        TempData["ErrorType"] = "success";
        TempData["ErrorMessage"] = "Produit modifié avec succès";
        return RedirectToAction("Index");
    }

}