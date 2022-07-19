using Xunit;

namespace ISP.Tests
{
    public class DogShould
    {
        private readonly FakeConsole _console;
        private readonly Dog _dog;

        public DogShould()
        {
            _console = new FakeConsole();
            _dog = new Dog(_console);
        }

        [Fact]
        public void Run()
        {
            _dog.Run();
            _console.ShouldContain("Dog is running");
        }

        [Fact]
        public void Bark()
        {
            _dog.Bark();
            _console.ShouldContain("Dog is barking");
        }
    }
}