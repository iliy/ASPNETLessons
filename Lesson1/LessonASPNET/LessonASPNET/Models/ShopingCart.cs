using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LessonASPNET.Models
{
    public class ShopingCart
    {
        private ILinqValueCalculator calc;
        
        public ShopingCart(ILinqValueCalculator calcParam)
        {
            calc = calcParam;
        }

        public IEnumerable<Product> products { get; set; }

        public decimal CalculateProductTotal()
        {
            return calc.ValueProducts(products);
        }
    }
}