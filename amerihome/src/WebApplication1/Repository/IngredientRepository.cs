using System.Collections.Generic;

namespace RecipeCalculator.Repository
{
    using Models;

    public class InMemoryIngredientRepository: IIngredientRepository
    {
        public InMemoryIngredientRepository()
        {
            _ingredients = new List<Ingredient>();
        }

        public IEnumerable<Ingredient> GetAll()
        {
            return _ingredients;
        }

        public void Add(Ingredient ingredient)
        {
            _ingredients.Add(ingredient);
            ingredient.Id = _ingredients.Count;
        }

        readonly List<Ingredient> _ingredients;
    }
}
