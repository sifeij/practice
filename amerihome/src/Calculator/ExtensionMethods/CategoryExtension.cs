using System.IO;

namespace Calculator
{
    using Models;

    public static class IngredientCategory
    {
        public static Category Map(this string str)
        {
            switch (str)
            {
                case "Produce":
                    return Category.Produce;
                case "Meat/poultry":
                    return Category.MeatPoultry;
                case "Pantry":
                    return Category.Pantry;
                default:
                    throw new InvalidDataException($"Unknown category {str}");
            }
        }
    }
}
