using System;
using static System.Math;

namespace Calculator
{
    public static class InvoiceExtension
    {
        public static decimal RoundUpTo7Cents(this decimal input)
        {
            if (input < 0)
            {
                throw new ArgumentException($"tax {input} cannot be negative");
            }
            var wholeNumber = Convert.ToDecimal(Floor(input));
            input = input - wholeNumber;
            var result = input % .07m == 0
                        ? input
                        : (Floor((input / 0.07m)) + 1) * 0.07m;
            return wholeNumber + result;
        }

        public static decimal RoundUpToCents(this decimal input)
        {
            if (input < 0)
            {
                throw new ArgumentException($"discount {input} cannot be negative");
            }
            return Ceiling(input * 100m) / 100;
        }
    }
}
