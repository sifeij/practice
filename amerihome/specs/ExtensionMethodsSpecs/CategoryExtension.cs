using System.IO;

using Xunit;

namespace specs
{
    using Calculator;

    public class CategoryExtensionSpecs
    {
        [Theory]
        [InlineData("Produce", "Produce")]
        [InlineData("Meat/poultry", "MeatPoultry")]
        [InlineData("Pantry", "Pantry")]
        public void Should_map_to_category(string input, string result)
        {
            var transformedResult = IngredientCategory.Map(input).ToString();
            Assert.Equal(result, transformedResult);
        }

        [Theory]
        [InlineData("invalid string")]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(null)]
        [Trait("Category", "Error Checking")]
        public void Should_throw_InvalidDataException_when_passing_invalid_string_or_null_to_map_to_category(string input)
        {
            Assert.Throws<InvalidDataException>(() => IngredientCategory.Map(input));
        }
    }
}
