using System;

namespace RecipeCalculator.ExtensionMethods
{
    public static class FractionExtension
    {
        public static double ParseToDouble(this string input)
        {
            double result = 0d;
            var numbers = input.Split(new Char[] { ' ', '/' });
            var itemCount = numbers.Length;

            if(itemCount < 2 || itemCount > 3)
            {
                throw new FormatException("Not a valid fraction format. Samples: 3/4, 7/3 or 3 5/8");
            }

            var numerator = 0d;
            var denominator = 0d;

            if (itemCount == 2)
            {
                numerator = Convert.ToDouble(numbers[0]);
                denominator = Convert.ToDouble(numbers[1]);
                result = Convert.ToDouble(numerator / denominator);
            }

            if(itemCount == 3)
            {
                var i = Convert.ToDouble(numbers[0]);
                numerator = Convert.ToDouble(numbers[1]);
                denominator = Convert.ToDouble(numbers[2]);
                result = i + Convert.ToDouble(numerator / denominator);
            }

            return result;
        }
    }
}
