using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using SportsStore.Components;
using SportsStore.Controllers;
using SportsStore.Models;
using SportsStore.ViewModels;
using Xunit;

namespace SportsStore.Test;

public class NavigationMenuViewComponentTest
{
    [Fact]
    public void Can_Filtr_Paginate()
    {
        Mock<IProductRepository> mock = new Mock<IProductRepository>();
        mock.Setup(m => m.Products).Returns(new Product[]
        {
            new Product {ProductID = 1, Name = "P1", Category = "1"},
            new Product {ProductID = 2, Name = "P2", Category = "1"},
            new Product {ProductID = 3, Name = "P3", Category = "2"},
            new Product {ProductID = 4, Name = "P4", Category = "3"},
            new Product {ProductID = 5, Name = "P5", Category = "2"},
        });
        NavigationMenuViewComponent component = new NavigationMenuViewComponent(mock.Object);

        string[] result =
            ((IEnumerable<string>) ((component.Invoke() as ViewViewComponentResult)!).ViewData!.Model!).ToArray();

            
       
        Assert.True(Enumerable.SequenceEqual(new string[] { "1","2","3" }, result));
       
    }
}