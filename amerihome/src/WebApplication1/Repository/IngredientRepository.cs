using System.Collections.Generic;
using System.Linq;

namespace RecipeCalculator.Repository
{
    using Models;

    public class IngredientRepository: IIngredientRepository
    {
        public IEnumerable<Ingredient> GetAll()
        {
            var result = new List<Ingredient>
            {
                new Ingredient {
                    Id = 1,
                    Name = "clove of organic garlic",
                    CategoryName = Category.Produce,
                    UnitPrice = 0.67d
                },
                new Ingredient {
                    Id = 2,
                    Name = "Lemon",
                    CategoryName = Category.Produce,
                    UnitPrice = 2.03d
                },
                new Ingredient {
                    Id = 3,
                    Name = "cup of corn",
                    CategoryName = Category.Produce,
                    UnitPrice = 0.87d
                },
                new Ingredient {
                    Id = 4,
                    Name = "chicken breast",
                    CategoryName = Category.MeatPoultry,
                    UnitPrice = 2.19d
                },
                new Ingredient {
                    Id = 5,
                    Name = "slice of bacon",
                    CategoryName = Category.MeatPoultry,
                    UnitPrice = 0.24d
                },
                new Ingredient {
                    Id = 6,
                    Name = "ounce of pasta",
                    CategoryName = Category.Pantry,
                    UnitPrice = 0.31d
                },
                new Ingredient {
                    Id = 7,
                    Name = "cup of organic olive oil",
                    CategoryName = Category.Pantry,
                    UnitPrice = 1.92d
                },
                new Ingredient {
                    Id = 8,
                    Name = "cup of vinegar",
                    CategoryName = Category.Pantry,
                    UnitPrice = 1.26d
                },
                new Ingredient {
                    Id = 9,
                    Name = "teaspoon of salt",
                    CategoryName = Category.Pantry,
                    UnitPrice = 0.16d
                },
                new Ingredient {
                    Id = 10,
                    Name = "teaspoon of pepper",
                    CategoryName = Category.Pantry,
                    UnitPrice = 0.17d
                }
            };

            return result;
        }

        public IEnumerable<Ingredient> GetByRecipes(IEnumerable<Recipe> recipes)
        {
            var result = new List<Ingredient>();

            foreach (var recipe in recipes)
            {
                Ingredient matchItem = GetAll().Where(g => g.Name == recipe.IngredientName)
                                               .Select(
                                                    i => new Ingredient {
                                                        Name = i.Name,
                                                        UnitPrice = i.UnitPrice,
                                                        TaxAmount = i.TaxAmount,
                                                        DiscountAmount = i.DiscountAmount
                                                    })
                                               .FirstOrDefault();
                if(matchItem != null)
                {
                    result.Add(matchItem);
                }
            }
            return result;
        }

        public RecipeCost CaculateCost(IEnumerable<Recipe> recipes)
        {
            var matchedIngredients = GetByRecipes(recipes);
            double tax = 0d;
            double discount = 0d;
            double total = 0d;

            if(matchedIngredients != null) {
                foreach (var recipe in recipes)
                {
                    tax = matchedIngredients.Sum(i => i.TaxAmount * recipe.Unit);
                    discount = matchedIngredients.Sum(i => i.DiscountAmount * recipe.Unit);
                    total = matchedIngredients.Sum(i => i.UnitPrice * recipe.Unit);
                }

                var result = new RecipeCost
                {
                    Tax = tax,
                    Discount = discount,
                    Total = total
                };
                return result;
            }
            return null;
        }
    }
}
