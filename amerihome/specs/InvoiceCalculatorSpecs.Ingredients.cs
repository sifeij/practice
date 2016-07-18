using System.Collections.Generic;

namespace specs
{
    using Calculator.Models;

    public partial class InvoiceCalculatorSpecs
    {
        static readonly List<Ingredient> Ingredients = new List<Ingredient>
            {
                new Ingredient {
                    Name = "clove of organic garlic",
                    CategoryName = Category.Produce,
                    UnitPrice = 0.67m
                },
                new Ingredient {
                    Name = "Lemon",
                    CategoryName = Category.Produce,
                    UnitPrice = 2.03m
                },
                new Ingredient {
                    Name = "cup of corn",
                    CategoryName = Category.Produce,
                    UnitPrice = 0.87m
                },
                new Ingredient {
                    Name = "chicken breast",
                    CategoryName = Category.MeatPoultry,
                    UnitPrice = 2.19m
                },
                new Ingredient {
                    Name = "slice of bacon",
                    CategoryName = Category.MeatPoultry,
                    UnitPrice = 0.24m
                },
                new Ingredient {
                    Name = "ounce of pasta",
                    CategoryName = Category.Pantry,
                    UnitPrice = 0.31m
                },
                new Ingredient {
                    Name = "cup of organic olive oil",
                    CategoryName = Category.Pantry,
                    UnitPrice = 1.92m
                },
                new Ingredient {
                    Name = "cup of vinegar",
                    CategoryName = Category.Pantry,
                    UnitPrice = 1.26m
                },
                new Ingredient {
                    Name = "teaspoon of salt",
                    CategoryName = Category.Pantry,
                    UnitPrice = 0.16m
                },
                new Ingredient {
                    Name = "teaspoon of pepper",
                    CategoryName = Category.Pantry,
                    UnitPrice = 0.17m
                }
            };
    }
}
