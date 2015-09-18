using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LessonASPNET.Models
{
    public class MinimumDiscountHelper:IDiscountHelper
    {
        public decimal ApplyDiscount(decimal totalParam)
        {
            if (totalParam < 0) throw new ArgumentOutOfRangeException();
            
            if (totalParam < 10) return totalParam;
            
            if (totalParam <= 100) return totalParam - 5m;

            return 0.9m * totalParam;
        }
    }
}