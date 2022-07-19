using System.Collections.Generic;
using FluentAssertions;

namespace ISP.Tests
{
    public class FakeConsole : IConsole
    {
        private readonly List<string> _console = new();

        public void WriteLine(string text) => _console.Add(text);

        public void ShouldContain(string text) =>
            _console
                .Contains(text)
                .Should()
                .BeTrue();
    }
}