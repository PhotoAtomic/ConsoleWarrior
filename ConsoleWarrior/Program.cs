using ConsoleWarrior.Drivers;
using System;

namespace ConsoleWarrior
{
    class Program
    {

        static void Main(string[] args)
        {

            IDriver driver = new ConsoleGraphic();

            World level = LevelLoader.Load(driver);
            var hero = new Hero(driver);
            var camera = new Camera(driver);
            IController player1 = new ConsoleController(hero);

            camera.Attach(level, level.Width / 2, level.Height / 2).Commit();
            hero.Attach(level, level.Width /2 , level.Height / 2).Commit();

            var lastTime = DateTime.Now;
            do
            {
                var current = DateTime.Now;
                player1.ProcessInput();

                var elapsed = current.Subtract(lastTime);
                camera.Render(elapsed.TotalMilliseconds);
                lastTime = current;
            } while (!player1.Exit);
        }
    }
}
