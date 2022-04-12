using System;
using System.IO;
using ApprovalTests;
using ApprovalTests.Reporters;
using Trivia;
using Xunit;


namespace Tests
{
    [UseReporter(typeof(DiffReporter))]
    public class GameTest
    {
        private readonly StringWriter _fakeConsole = new();
        public GameTest()
        {
            Console.SetOut(_fakeConsole);
        }

        [Fact]
        public void Test1()
        {
            var game = new Game();
            game.Add("Bart");
            game.Roll(12);
            game.WrongAnswer();
            game.Roll(2);
            game.Roll(13);
            game.WasCorrectlyAnswered();
            game.Roll(13);
            
            ApproveConsole();
        }
        
        [Fact]
        public void Test2()
        {
            var game = new Game();
            game.Add("Bart");
            game.Add("Lisa");
            game.Roll(1);
            game.WasCorrectlyAnswered();
            game.Roll(2);
            game.WasCorrectlyAnswered();
            game.Roll(2);
            game.WasCorrectlyAnswered();
            game.Roll(2);
            game.WasCorrectlyAnswered();
            game.Roll(2);
            game.WasCorrectlyAnswered();
            game.Roll(2);
            game.WasCorrectlyAnswered();
            game.Roll(2);
            game.WasCorrectlyAnswered();
            game.Roll(2);
            game.WasCorrectlyAnswered();
            game.Roll(2);
            game.WasCorrectlyAnswered();
            game.Roll(2);
            game.WasCorrectlyAnswered();
            
            ApproveConsole();
        }
        
        [Fact]
        public void Test3()
        {
            var game = new Game();
            game.Add("Bart");
            game.Add("Lisa");
            game.Roll(1);
            game.WrongAnswer();
            game.Roll(2);
            game.WasCorrectlyAnswered();
            game.Roll(2);
            game.WasCorrectlyAnswered();
            game.Roll(2);
            game.WasCorrectlyAnswered();
            game.Roll(2);
            game.WasCorrectlyAnswered();
            game.Roll(2);
            game.WasCorrectlyAnswered();
            game.Roll(2);
            game.WasCorrectlyAnswered();
            game.Roll(2);
            game.WasCorrectlyAnswered();
            game.Roll(2);
            game.WasCorrectlyAnswered();
            game.Roll(2);
            game.WasCorrectlyAnswered();
            
            ApproveConsole();
        }

        private void ApproveConsole()
        {
            Approvals.Verify(_fakeConsole.ToString());
        }
    }
}
