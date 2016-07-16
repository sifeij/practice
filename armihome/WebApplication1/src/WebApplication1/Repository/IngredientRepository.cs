using System.Collections.Generic;
using System.Linq;

namespace RecipeCalculator.Repository
{
    using Models;

    public class IngredientRepository
    {
        public IEnumerable<Ingredient> GetAll()
        {
            var result = new List<Ingredient>
            {
                new Ingredient {
                    Name = "clove of organic garlic",
                    CategoryName = Category.Produce,
                    UnitPrice = 0.67d
                },
                new Ingredient {
                    Name = "Lemon",
                    CategoryName = Category.Produce,
                    UnitPrice = 2.03d
                },
                new Ingredient {
                    Name = "cup of corn",
                    CategoryName = Category.Produce,
                    UnitPrice = 0.87d
                },
                new Ingredient {
                    Name = "chicken breast",
                    CategoryName = Category.MeatPoultry,
                    UnitPrice = 2.19d
                },
                new Ingredient {
                    Name = "slice of bacon",
                    CategoryName = Category.MeatPoultry,
                    UnitPrice = 0.24d
                },
                new Ingredient {
                    Name = "ounce of pasta",
                    CategoryName = Category.Pantry,
                    UnitPrice = 0.31d
                },
                new Ingredient {
                    Name = "cup of organic olive oil",
                    CategoryName = Category.Pantry,
                    UnitPrice = 1.92d
                },
                new Ingredient {
                    Name = "cup of vinegar",
                    CategoryName = Category.Pantry,
                    UnitPrice = 1.26d
                },
                new Ingredient {
                    Name = "teaspoon of salt",
                    CategoryName = Category.Pantry,
                    UnitPrice = 0.16d
                },
                new Ingredient {
                    Name = "teaspoon of pepper",
                    CategoryName = Category.Pantry,
                    UnitPrice = 0.17d
                }
            };

            return result;
        }

        public Ingredient GetByName(string name)
        {
            var result = GetAll().Where(g => g.Name == name)
                                 .FirstOrDefault();
            return result;
        }
    }
}
