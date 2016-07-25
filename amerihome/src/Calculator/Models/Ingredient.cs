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
                    UnitPrice * TaxRate;
                return result;
            }
        }

        public decimal DiscountAmount 
        {
            get
            {
                var result = Name.ToLowerInvariant().Contains("organic") 
                    ? UnitPrice * DiscountRate : 
                    0m;
                return result;
            }
        }

        public override string ToString() => 
            $"{CategoryName}: {Name} => ${UnitPrice} Tax: ${TaxAmount}";


        public const decimal TaxRate      = 0.086m;
        public const decimal DiscountRate = 0.05m;

        decimal _taxAmount;
        decimal _discountAmount;
    }
}
