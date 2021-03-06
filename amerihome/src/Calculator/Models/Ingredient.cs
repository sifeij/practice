﻿namespace Calculator.Models
{
    public class Ingredient
    {
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public Category CategoryName { get; set; }

        public decimal TaxAmount
        {
            get
            {
                return CategoryName == Category.Produce ? 0m : UnitPrice * TaxRate;
            }
        }

        public decimal DiscountAmount
        {
            get
            {
                var name = Name?.ToLowerInvariant();
                return name.Contains("organic") ? UnitPrice * DiscountRate : 0m;
            }
        }

        public override string ToString()
        {
            return $"{CategoryName}: {Name} => ${UnitPrice} Tax: ${TaxAmount}";
        }

        public const decimal TaxRate = 0.086m;
        public const decimal DiscountRate = 0.05m;

        decimal _taxAmount;
        decimal _discountAmount;
    }
}
