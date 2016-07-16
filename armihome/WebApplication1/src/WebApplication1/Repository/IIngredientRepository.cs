using System.Collections.Generic;

namespace RecipeCalculator.Repository
{
    using Models;

    interface IIngredientRepository
    {
        IEnumerable<Ingredient> GetAll();
        Ingredient GetByName();
    }
}
