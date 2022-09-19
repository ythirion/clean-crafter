using FluentAssertions;
using Xunit;

namespace stack_kata;

public class RomanNumeralsTest
{
    [Fact]
    public void AFirstTest()
    {
        43.Should()
            .Be(42, "you need to implement your tests here");
    }
}