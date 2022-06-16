using System.Linq;
using SportsStore.Models;
using Xunit;

namespace SportsStore.Test
{
    public class CartTest
    {
        [Fact]
        public void Can_Add_New_Lines()
        {
            Product p1 = new Product {ProductID = 1, Name = "P1"};
            Product p2 = new Product {ProductID = 2, Name = "P2"};

            Cart target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            CartLine[] results = target.Lines.ToArray();

            Assert.Equal(2, results.Length);
            Assert.Equal(p1, results[0].Product);
            Assert.Equal(p2, results[1].Product);
        }

        [Fact]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            Product p1 = new Product {ProductID = 1, Name = "P1"};
            Product p2 = new Product {ProductID = 2, Name = "P2"};

            Cart target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);

            CartLine[] results = target.Lines.OrderBy(c => c.Product.ProductID).ToArray();

            Assert.Equal(2, results.Length);
            Assert.Equal(11, results[0].Quantity);
            Assert.Equal(1, results[1].Quantity);
        }

        [Fact]
        public void Can_Remuve_Lien()
        {
            Product p1 = new Product {ProductID = 1, Name = "P1"};
            Product p2 = new Product {ProductID = 2, Name = "P2"};
            Product p3 = new Product {ProductID = 3, Name = "P3"};

            Cart target = new Cart();
            
            target.AddItem(p1,1);
            target.AddItem(p2,3);
            target.AddItem(p3,5);
            target.AddItem(p3,1);
            
            target.RemoveLine(p2);
            
            Assert.Equal(0, target.Lines.Where(c => c.Product == p2).Count());
            Assert.Equal(2,target.Lines.Count());
        }

        [Fact]
        public void Calculate_Cart_Totel()
        {
            Product p1 = new Product {ProductID = 1, Name = "P1", Price = 10M};
            Product p2 = new Product {ProductID = 2, Name = "P2", Price = 10M};
            Product p3 = new Product {ProductID = 3, Name = "P3", Price = 10M};

            Cart target = new Cart();
            
            target.AddItem(p1,2);
            target.AddItem(p2,1);
            target.AddItem(p3,2);

            decimal total =  target.ComputeTotalValue();
            
            Assert.Equal(50M, total);
        }

        [Fact]
        public void Can_Cart_Condents()
        {
            Product p1 = new Product {ProductID = 1, Name = "P1", Price = 10M};
            Product p2 = new Product {ProductID = 2, Name = "P2", Price = 10M};
            Product p3 = new Product {ProductID = 3, Name = "P3", Price = 10M};

            Cart target = new Cart();
            
            target.AddItem(p1,2);
            target.AddItem(p2,1);
            target.AddItem(p3,2);

            target.Clear();
            
            Assert.Equal(0, target.Lines.Count());
        }
    }
}