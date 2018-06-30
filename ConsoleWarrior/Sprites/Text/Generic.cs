using ConsoleWarrior.Drivers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleWarrior.Sprites.Text
{


    public class Generic : ICharSprite
    {

        public string Text { get; }

        public Generic(string text)
        {
            Text = text;
        }        

        public void Draw(IDrawer driver, int x, int y)
        {
            driver.Draw(Text, x, y);
        }

    }
}
