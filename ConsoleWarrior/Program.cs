using System;

namespace ConsoleWarrior
{
    class Program
    {

        static void Main(string[] args)
        {
            World level = LevelLoader.Load();
            var hero = new Hero();
            IController player1 = new ConsoleController(hero);
            hero.Attach(level, level.Width /2 , level.Height / 2);

            do
            {
                player1.ProcessInput();
                level.Update();
                level.Render();
            } while (!player1.Exit);
        }
    }
}
