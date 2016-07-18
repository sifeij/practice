using System.Collections.Generic;

namespace Calculator.Services
{
    using Models;
    using Recipe = Dictionary<string, decimal>;

    public interface IInvoiceCalculator
    {
        Invoice CalculateCost(Recipe recipe);
    }
}
