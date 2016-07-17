using System.Collections.Generic;
using System.Linq;

namespace RecipeCalculator.Services
{
    using Models;
    using Repository;
    using Recipe = Dictionary<int, double>;

    public class InvoiceCalculator : IInvoiceCalculator
    {
        public InvoiceCalculator(IIngredientRepository repository)
        {
            _repository = repository;
        }

        public Invoice CaculateCost(Recipe recipe)
        {
            var matchedIngredients = recipe.Zip(_repository.GetAll(), 
                                                (r, i) =>
                                                {
                                                    if (r.Key == i.Id)
                                                    {
                                                        return new { Ingredient = i, Quantity = r.Value };
                                                    }
                                                    return null;
                                                });

            var invoice = new Invoice
            {
                Tax      = matchedIngredients.Sum(m => m.Ingredient.TaxAmount * m.Quantity),
                Discount = matchedIngredients.Sum(m => m.Ingredient.DiscountAmount * m.Quantity),
            };

            invoice.Total = matchedIngredients.Sum(m => m.Ingredient.UnitPrice * m.Quantity) +
                            invoice.Tax +
                            invoice.Discount;

            return invoice;
        }

        readonly IIngredientRepository _repository;
    }
}
