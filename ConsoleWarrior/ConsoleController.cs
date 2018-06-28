using System;

namespace ConsoleWarrior
{
    internal class ConsoleController : IController
    {
        private Hero hero;

        public ConsoleController(Hero hero)
        {
            this.hero = hero;
            Console.CancelKeyPress += Console_CancelKeyPress;
        }

        private void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Exit = true;
        }

        public bool Exit { get; private set;  }

        
        public void ProcessInput()
        {
            

            if (!Console.KeyAvailable) return;
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.LeftArrow) hero?.Left();
            if (key == ConsoleKey.UpArrow) hero?.Up();
            if (key == ConsoleKey.RightArrow) hero?.Right();
            if (key == ConsoleKey.DownArrow) hero?.Down();
            if (key == ConsoleKey.Spacebar) hero?.Fire();
            if (key == ConsoleKey.Escape) Exit = true;
        }
    }
}