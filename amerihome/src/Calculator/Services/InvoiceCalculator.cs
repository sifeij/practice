using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Services
{
    using Models;
    using Repository;
    using Recipe = Dictionary<string, decimal>;

    public class InvoiceCalculator : IInvoiceCalculator
    {
        public InvoiceCalculator(IIngredientRepository repository)
        {
            _repository = repository;
        }

        public Invoice CalculateCost(Recipe recipe)
        {
            //if(recipe == null)
            //{
            //    throw new ArgumentNullException("recipe");
            //}
            var matchedIngredients = recipe?.Join(_repository.GetAll(),
                                            r => r.Key,
                                            i => i.Name,
                                            (r, i) => new { Ingredient = i, Quantity = r.Value });

            var invoice = new Invoice
            {
                Tax = matchedIngredients.Sum(m => m.Ingredient.TaxAmount * m.Quantity)
                                                .RoundUpTo7Cents(),
                Discount = matchedIngredients.Sum(m => m.Ingredient.DiscountAmount * m.Quantity)
                                                .RoundUpToCents(),
            };

            invoice.Total = matchedIngredients.Sum(m => m.Ingredient.UnitPrice * m.Quantity)
                            + invoice.Tax
                            - invoice.Discount;

            return invoice;
        }

        readonly IIngredientRepository _repository;
    }
}
