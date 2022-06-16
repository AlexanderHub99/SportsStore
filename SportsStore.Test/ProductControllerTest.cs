using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
            ProductController controller = new ProductController(mock.Object);
            controller.pageSixe = 3;

           Product[] result = ((controller.List("2", 1).ViewData.Model as ProductListViewModel)!).Products.ToArray();

            
            Assert.Equal(2, result.Length);
            Assert.True(result[0].Name == "P3" && result[0].Category == "2");
            Assert.True(result[1].Name == "P5" && result[1].Category == "2");
        }
        [Fact]
        public void Generate_Category_Specific_Product_Count()
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
            ProductController controller = new ProductController(mock.Object);
            controller.pageSixe = 3;

            Func<ViewResult, ProductListViewModel?> getMode = result => result?.ViewData?.Model as ProductListViewModel;

            int? res1 = getMode(controller.List(("1")))?.pagingInfo.TotalItems;
            int? res2 = getMode(controller.List(("2")))?.pagingInfo.TotalItems;
            int? res3 = getMode(controller.List(("3")))?.pagingInfo.TotalItems;
            int? resAll = getMode(controller.List((null!)))?.pagingInfo.TotalItems;
            
            Assert.Equal(2, res1);
            Assert.Equal(2, res2);
            Assert.Equal(1, res3);
            Assert.Equal(5, resAll);
        }
    }
}