using ConsoleWarrior.Drivers;
using System;

namespace ConsoleWarrior
{
    class Program
    {

        static void Main(string[] args)
        {

            IDriver driver = new ConsoleGraphic();

            World level = LevelLoader.Load();
            var hero = new Hero(driver);
            var camera = new Camera(driver);
            IController player1 = new ConsoleController(hero);

            camera.Attach(level, level.Width / 2, level.Height / 2).Commit();
            hero.Attach(level, level.Width /2 , level.Height / 2).Commit();

            do
            {
                player1.ProcessInput();
                
                camera.Render();
            } while (!player1.Exit);
        }
    }
}
