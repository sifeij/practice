using System;
using System.Collections.Generic;

namespace RecipeCalculator.Repository
{
    using Models;

    public interface IIngredientRepository
    {
        IEnumerable<Ingredient> GetAll();
        void Add(Ingredient item);
    }
}
