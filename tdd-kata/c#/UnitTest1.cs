using FluentAssertions;
using Xunit;

namespace stack_kata;

public class UnitTest1
{
    [Fact]
    public void Test1() => 43.Should().Be(42);
}