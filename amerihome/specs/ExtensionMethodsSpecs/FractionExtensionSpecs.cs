using System;
using Xunit;

namespace specs
{
    using Calculator;

    public class FractionExtensionSpecs
    {
        [Theory]
        [InlineData("0")]
        [InlineData("1")]
        [InlineData("11")]
        [InlineData("101")]
        public void Should_convert_whole_number_to_decimal(string input)
        {
            decimal result = input.ParseToDecimal();
            Assert.Equal(Convert.ToInt32(input) * 1m, result);
        }

        [Theory]
        [InlineData("3/4", 0.75)]
        [InlineData("13 1/4", 13.25)]
        public void Should_convert_fraction_to_decimal(string input, decimal expected)
        {
            decimal result = input.ParseToDecimal();
            Assert.Equal(expected, result);
        }

        [Fact]
        [InlineData("2 5/3")]
        public void Should_convert_2_and_1_third_fraction_to_decimal()
        {
            var fraction = "2 5/3";
            decimal result = fraction.ParseToDecimal();
            Assert.Equal(2m + (5m / 3m), result);
        }

        [Theory]
        [InlineData("0.11")]
        [InlineData("0.000000000000000000000011")]
        [InlineData("101011110001110000000000")]
        public void Should_convert_decimal_to_decimal(string input)
        {
            decimal result = input.ParseToDecimal();
            Assert.Equal(Convert.ToDecimal(input) * 1m, result);
        }

        [Theory(DisplayName = "Passing invalid string that contains letter, empty or one space string")]
        [InlineData("a")]
        [InlineData("one")]
        [InlineData("two strings")]
        [InlineData("")]
        [InlineData(" ")]
        public void Should_throw_FormatException_when_passing_letters_to_convert_to_decimal(string input)
        {
            Assert.Throws<FormatException>(() => input.ParseToDecimal());
        }

        [Fact]
        public void Should_throw_ArgumentNullException_when_convert_to_decimal()
        {
            string input = null;
            Assert.Throws<ArgumentNullException>(() => input.ParseToDecimal());
        }
    }
}
