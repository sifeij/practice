using System;

using Xunit;

namespace specs
{
    using Calculator;

    public class InvoiceExtensionSpecs
    {
        [Theory(DisplayName = "Passing valid value for tax")]
        [InlineData(0.00001, 0.07)]
        [InlineData(0.62, 0.63)]
        [InlineData(11.61, 11.63)]
        [InlineData(0, 0)]
        public void Should_round_up_to_7_cents(decimal input, decimal expected)
        {
            decimal result = input.RoundUpTo7Cents();
            Assert.Equal(expected, result);
        }

        [Theory(DisplayName = "Passing valid value for discount")]
        [InlineData(2.601, 2.61)]
        [InlineData(0.409, 0.41)]
        [InlineData(0, 0)]
        public void Should_round_up_to_260_cents(decimal input, decimal expected)
        {
            decimal result = input.RoundUpToCents();
            Assert.Equal(expected, result);
        }

        [Fact]
        [Trait("Category", "Error Checking")]
        public void Should_throw_ArgumentException_when_passing_negative_number_to_round_up_to_7_cents()
        {
            var input = -2.3m;
            Assert.Throws<ArgumentException>(() => input.RoundUpTo7Cents());
        }

        [Fact]
        [Trait("Category", "Error Checking")]
        public void Should_throw_ArgumentException_when_passing_negative_number_to_round_up_to_cents()
        {
            var input = -1.366666m;
            Assert.Throws<ArgumentException>(() => input.RoundUpToCents());
        }
    }
}
