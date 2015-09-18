using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LessonASPNET.Models
{
    public interface ILinqValueCalculator
    {
        decimal ValueProducts(IEnumerable<Product> products);
    }
    public class LinqValueCalculator:ILinqValueCalculator
    {
        private IDiscountHelper discountHelper;

        public LinqValueCalculator(IDiscountHelper discountParam)
        {
            discountHelper = discountParam;
        }

        public decimal ValueProducts(IEnumerable<Product> products)
        {
            return discountHelper.ApplyDiscount(products.Sum(p => p.Price));
        }
    }
}