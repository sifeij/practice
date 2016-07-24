namespace Calculator.Models
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
                var result = CategoryName == Category.Produce 
                    ? 0m : 
                    UnitPrice * TAX_RATE;
                return result;
            }
        }

        public decimal DiscountAmount
        {
            get
            {
                var result = Name.ToLowerInvariant().Contains("organic") 
                    ? UnitPrice * DISCOUNT_RATE : 
                    0m;
                return result;
            }
        }

        public override string ToString() => 
            $"{CategoryName}: {Name} => ${UnitPrice} Tax: ${TaxAmount}";

        decimal       _taxAmount;
        decimal       _discountAmount;
        const decimal TAX_RATE = 0.086m;
        const decimal DISCOUNT_RATE = 0.05m;
    }
}
