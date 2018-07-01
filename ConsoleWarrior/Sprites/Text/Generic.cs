using ConsoleWarrior.Drivers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleWarrior.Sprites.Text
{


    public class Generic : ICharSprite
    {



        public readonly string text;
        private readonly ConsoleColor foregroundColor;
        private readonly ConsoleColor backgroundColor;

        public Generic(string text, ConsoleColor foregroundColor = ConsoleColor.Gray, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            this.text = text;
            this.foregroundColor = foregroundColor;
            this.backgroundColor = backgroundColor;
        }        

        public void Draw(IDrawer driver, int x, int y)
        {
            driver.Draw(text, x, y, foregroundColor, backgroundColor);
        }

    }
}
