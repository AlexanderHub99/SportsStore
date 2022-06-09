using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportsStore.Models;

namespace SportsStore.Components
{
    public class NavigationMenuViewComponent: ViewComponent
    {
        private IProductRepository _productRepository;

        public NavigationMenuViewComponent(IProductRepository repository)
        {
            _productRepository = repository;
        }

        //Кудато потерялася мето файл :(                                                                    :todo
        // ReSharper disable once Mvc.ViewComponentViewNotResolved                                          :todo
        public IViewComponentResult Invoke() => View(_productRepository.Products
            .Select(x => x.Category)
            .Distinct()
            .OrderBy(x => x));
    }
}

