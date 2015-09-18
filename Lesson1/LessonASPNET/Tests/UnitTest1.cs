using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LessonASPNET.Models;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        private IDiscountHelper GetTestObject()
        {
            return new MinimumDiscountHelper();
        }

        [TestMethod]
        public void Discount_Above_100()
        {
            IDiscountHelper target = GetTestObject();
            decimal total = 200;
            decimal discountTotal = target.ApplyDiscount(total);
            Assert.AreEqual(total * 0.9m, discountTotal);
        }

        [TestMethod]
        public void Discount_Above_10_Between_100()
        {
            IDiscountHelper target = GetTestObject();
            
            decimal total100Discount = target.ApplyDiscount(100);
            decimal total10Discount = target.ApplyDiscount(10);
            decimal total50Discount = target.ApplyDiscount(50);

            Assert.AreEqual(95, total100Discount, "Total 100 discount are wrong!");
            Assert.AreEqual(45, total50Discount, "Total 50 discount are wrong!");
            Assert.AreEqual(5, total10Discount, "Total 10 discount are wrong!");
        }

        [TestMethod]
        public void Discount_Less_Then_10()
        {
            IDiscountHelper target = GetTestObject();

            decimal total5Discount = target.ApplyDiscount(5);
            decimal total0Discount = target.ApplyDiscount(0);

            Assert.AreEqual(5, total5Discount, "Total 5 discount are wrong!");
            Assert.AreEqual(0, total0Discount, "Total 0 discount are wrong!");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Discount_Negative()
        {
            IDiscountHelper target = GetTestObject();
            target.ApplyDiscount(-1);
        }
    }
}
