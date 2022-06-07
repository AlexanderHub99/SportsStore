using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        
        public ProductController(IProductRepository logger)
        {
            _productRepository = logger;
        }

        private readonly int _pageSixe = 4;
    
        public ViewResult List(int page = 1) => View(_productRepository.Products
            .OrderBy(p => p.ProductID)
            .Skip((page -1)*_pageSixe)
            .Take(_pageSixe));
    }
}

