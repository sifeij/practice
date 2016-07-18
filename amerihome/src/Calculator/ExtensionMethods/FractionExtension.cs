using System;

namespace Calculator
{
    public static class FractionExtension
    {
        public static decimal ParseToDecimal(this string input)
        {
            if(input == null)
            {
                throw new ArgumentNullException("cannot parse null to decimal");
            }
            var numbers = input.Split(new Char[] { ' ', '/' });
            var itemCount = numbers.Length;

            if (itemCount == 1)
            {
                return Convert.ToDecimal(input);
            }

            var numerator = 0m;
            var denominator = 0m;

            if (itemCount == 2)
            {
                numerator = Convert.ToDecimal(numbers[0]);
                denominator = Convert.ToDecimal(numbers[1]);
                return Convert.ToDecimal(numerator / denominator);
            }

            if (itemCount == 3)
            {
                var i = Convert.ToDecimal(numbers[0]);
                numerator = Convert.ToDecimal(numbers[1]);
                denominator = Convert.ToDecimal(numbers[2]);
                return i + Convert.ToDecimal(numerator / denominator);
            }
            throw new FormatException("Not a valid fraction format. Samples: 3/4, 7/3 or 3 5/8");
        }
    }
}
