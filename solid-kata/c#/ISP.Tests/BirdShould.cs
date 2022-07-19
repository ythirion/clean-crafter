using Xunit;

namespace ISP.Tests
{
    public class BirdShould
    {
        private readonly FakeConsole _console;
        private readonly Bird _bird;

        public BirdShould()
        {
            _console = new FakeConsole();
            _bird = new Bird(_console);
        }

        [Fact]
        public void Run()
        {
            _bird.Run();
            _console.ShouldContain("Bird is running");
        }

        [Fact]
        public void Fly()
        {
            _bird.Fly();
            _console.ShouldContain("Bird is flying");
        }
    }
}