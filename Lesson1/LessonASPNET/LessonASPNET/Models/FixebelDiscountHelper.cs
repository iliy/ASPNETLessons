using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LessonASPNET.Models
{
    public class FixebelDiscountHelper:IDiscountHelper
    {
        public decimal ApplyDiscount(decimal totalParam)
        {
            decimal discount = totalParam > 100 ? 70 : 50;
            return totalParam - (discount / 100m * totalParam);
        }
    }
}