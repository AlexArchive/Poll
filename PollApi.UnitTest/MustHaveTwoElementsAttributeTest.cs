using Xunit;
using Xunit.Extensions;

namespace PollApi.UnitTest
{
    public class MustHaveTwoElementsAttributeTest
    {
        [Theory]
        [InlineData(new[] { "foo", "bar" }, true)]
        [InlineData(new[] { "", "foo" }, false)]
        [InlineData(new[] { "", "" }, false)]
        [InlineData(new string[0], false)]
        public void IsValidReturnsCorrectResult(string[] value, bool expected)
        {
            var sut = new MustHaveTwoElementsAttribute();
            var actual = sut.IsValid(value);
            Assert.Equal(expected, actual);
        }
    }
}