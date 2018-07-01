using System;

namespace ConsoleWarrior.Drivers
{
    public interface IDrawer
    {
        void Draw(string text, int x, int y, ConsoleColor foreground = ConsoleColor.Gray, ConsoleColor background = ConsoleColor.Black);
    }
}