using System.Collections.Generic;

namespace RecipeCalculator.Services
{
    using Models;
    using Recipe = Dictionary<int, double>;
    public interface IInvoiceCalculator
    {
        Invoice CaculateCost(Recipe recipe);
    }
}
