namespace RecipeCalculator.Models
{
    public class Ingredient
    {
        public Ingredient()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double UnitPrice { get; set; } = 0d;
        public Category CategoryName { get; set; } = new Category();

        public double TaxAmount
        {
            get
            {
                var result = CategoryName == Category.Produce 
                    ? 0d : 
                    UnitPrice * TAX_RATE;
                return result;
            }
            set
            {
                _taxAmount = value;
            }
        }

        public double DiscountAmount
        {
            get
            {
                var result = Name.ToLowerInvariant().Contains("organic") 
                    ? UnitPrice * DISCOUNT_RATE : 
                    0;
                return result;
            }
            set
            {
                _discountAmount = value;
            }
        }

        double _taxAmount;
        double _discountAmount;
        const double TAX_RATE = 0.086;
        const double DISCOUNT_RATE = -0.05;
    }
}
