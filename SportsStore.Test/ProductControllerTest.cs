using System.Collections.Generic;
using System.Linq;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using SportsStore.ViewModels;
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
            });
            ProductController controller = new ProductController(mock.Object);
            controller.pageSixe = 3;

            ProductListViewModel? result = controller.List(null,2).ViewData.Model as ProductListViewModel;

            Product[] productArrey = result.Products.ToArray();
            Assert.True(productArrey.Length == 2);
            Assert.Equal("P4", productArrey[0].Name);
            Assert.Equal("P5", productArrey[1].Name);
        }
        [Fact]
        public void Can_Send_Pagination_View_Model()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"},
            });
            ProductController controller = new ProductController(mock.Object);
            controller.pageSixe = 3;

            ProductListViewModel? result = controller.List(null,2).ViewData.Model as ProductListViewModel;

            PagingInfo productArrey = result.pagingInfo;
            Assert.Equal(2, productArrey.CurrentPage);
            Assert.Equal(3, productArrey.InemsPerPage);
            Assert.Equal(5, productArrey.TotalItems);
            Assert.Equal(2, productArrey.TotalPages);
        }
    }
}