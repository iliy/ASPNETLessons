using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LessonASPNET.Models;
using System.Linq;
using Moq;

namespace Tests
{
    [TestClass]
    public class UnitTest2
    {
        private Product[] products = { 
                                         new Product { Name = "Kayak", Category = "Watersports", Price = 275M }, 
                                         new Product { Name = "Lifejacket", Category = "Watersports", Price = 48.95M }, 
                                         new Product { Name = "Soccer ball", Category = "Soccer", Price = 19.50M }, 
                                         new Product { Name = "Corner flag", Category = "Soccer", Price = 34.95M } 
                                     }; 
        
        [TestMethod]
        public void Sum_Products_Corretly()
        {
            Mock<IDiscountHelper> mock = new Mock<IDiscountHelper>();
            mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>()))
                .Returns<decimal>(total => total);

            var target = new LinqValueCalculator(mock.Object);
            var result = target.ValueProducts(products);

            Assert.AreEqual(result, products.Sum(p => p.Price));
        }

        private Product[] createProduct(decimal price)
        {
            return new[] { new Product { Price = price } };
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void Pass_Through_Variable_Discounts()
        {
            Mock<IDiscountHelper> mock = new Mock<IDiscountHelper>();
            // Условие, если ничего другое не подходит
            mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>()))
                .Returns<decimal>(total => total);
            // Условие, если переданное значение не является положительным
            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(p => p <= 0)))
                .Throws<System.ArgumentOutOfRangeException>();
            // Условие, если переданное значение больше 100
            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(p => p > 100)))
                .Returns<decimal>(total => total * 0.9m);
            // Условие, если переданное значение входит в интервал от 10 до 100 включительно
            mock.Setup(m => m.ApplyDiscount(It.IsInRange<decimal>(10, 100, Range.Inclusive)))
                .Returns<decimal>(total => total - 5);

            var target = new LinqValueCalculator(mock.Object);

            // Создание тестируемых значений
            decimal FiveDollarsDiscount = target.ValueProducts(createProduct(5));
            decimal TenDollarsDiscount = target.ValueProducts(createProduct(10));
            decimal FiftenDollarsDiscount = target.ValueProducts(createProduct(50));
            decimal OneHandredDollarsDiscount = target.ValueProducts(createProduct(100));
            decimal TwoHandredDollarsDiscount = target.ValueProducts(createProduct(200));

            Assert.AreEqual(5, FiveDollarsDiscount, "Five dollars discount wrong");
            Assert.AreEqual(5, TenDollarsDiscount, "Five dollars discount wrong");
            Assert.AreEqual(45, FiftenDollarsDiscount, "Five dollars discount wrong");
            Assert.AreEqual(95, OneHandredDollarsDiscount, "Five dollars discount wrong");
            Assert.AreEqual(180, TwoHandredDollarsDiscount, "Five dollars discount wrong");
            target.ValueProducts(createProduct(0));
        }
    }
}
