using System.Collections.Generic;
using System.Linq;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using Xunit;

namespace SportsStore.Test
{
    public class ProductControllerTest
    {
        [Fact]
        public void Can_Paginate()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"},
                new Product {ProductID = 6, Name = "P6"},
            });
            ProductController controller = new ProductController(mock.Object);
            controller.pageSixe = 3;

            IEnumerable<Product>? result = controller.List(2).ViewData.Model as IEnumerable<Product>;

            Product[] productArrey = result.ToArray();
            Assert.True(productArrey.Length == 2);
            Assert.Equal("P4", productArrey[0].Name);
            Assert.Equal("P4", productArrey[0].Name);
        }
    }
}