using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers;

public class ProductController : Controller
{
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository logger)
    {
        _productRepository = logger;
    }

    public ViewResult List() => View(_productRepository.Products);
}