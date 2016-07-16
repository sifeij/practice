using System.Collections.Generic;

namespace RecipeCalculator.Repository
{
    using Models;

    public interface IIngredientRepository
    {
        IEnumerable<Ingredient> GetAll();
        IEnumerable<Ingredient> GetByRecipes(IEnumerable<Recipe> recipes);
        RecipeCost CaculateCost(IEnumerable<Recipe> recipes);
    }
}
