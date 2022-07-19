namespace ISP
{
    public class Dog : IAnimal
    {
        private readonly IConsole _console;

        public Dog(IConsole console) => _console = console;

        public void Fly()
        {
        }

        public void Run() => _console.WriteLine("Dog is running");
        public void Bark() => _console.WriteLine("Dog is barking");
    }
}