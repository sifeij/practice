using System;

namespace Calculator.Models
{
    public class Invoice
    {
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }

        public override String ToString() => 
            $"\n\tTax=${Tax:0.00}\n\tDiscount=(${Discount:0.00})\n\t${Total:0.00}";
    }
}
