using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.ViewModels;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository logger)
        {
            _productRepository = logger;
        }

        public int pageSixe = 4;

        public ViewResult List(string category , int page = 1)
        {

            return View(new ProductListViewModel
            {
                Products = _productRepository.Products
                    .Where(p => category == null||p.Category == category)
                    .OrderBy(p => p.ProductID)
                    .Skip((page - 1) * pageSixe)
                    .Take(pageSixe),
                pagingInfo = new PagingInfo
                    {CurrentPage = page, InemsPerPage = pageSixe, TotalItems = _productRepository.Products.Count()},
                CurrentCategory = category
            });
        }
    }
}