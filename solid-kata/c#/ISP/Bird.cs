namespace ISP
{
    public class Bird : IAnimal
    {
        private readonly IConsole _console;

        public Bird(IConsole console) => _console = console;

        public void Bark()
        {
        }

        public void Run() => _console.WriteLine("Bird is running");
        public void Fly() => _console.WriteLine("Bird is flying");
    }
}