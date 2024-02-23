using System;
using System.Collections.Generic;

namespace BO
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CalculationUnit { get; set; }
        public double Quantity { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
