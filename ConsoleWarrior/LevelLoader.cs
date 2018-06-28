using System;

namespace ConsoleWarrior
{
    internal class LevelLoader
    {
        internal static World Load()
        {
            return new World(100,100);
        }
    }
}