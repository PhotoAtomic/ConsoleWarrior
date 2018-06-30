using ConsoleWarrior.Objects;
using System;

namespace ConsoleWarrior
{
    internal class LevelLoader
    {
        internal static World Load(IDriver driver)
        {
            var world =  new World(100,100);
            var torch = new Torch(driver);
            torch.Attach(world, 50, 51);
            return world;
        }
    }
}