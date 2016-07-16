namespace RecipeCalculator.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double UnitPrice { get; set; }
        public Category CategoryName { get; set; }

        public bool IsTaxable
        {
            get
            {
                return CategoryName == Category.Produce ? false : true;
            }
        }
        public bool IsOrganic
        {
            get
            {
                return Name.ToLowerInvariant().Contains("organic") ? true : false;
            }
        }
    }
}
