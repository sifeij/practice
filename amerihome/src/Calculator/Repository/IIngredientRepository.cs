using System.Collections.Generic;

namespace Calculator.Repository
{
    using Models;

    public interface IIngredientRepository
    {
        IEnumerable<Ingredient> GetAll();
        void Add(Ingredient item);
    }
}
