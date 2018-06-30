using ConsoleWarrior.Drivers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleWarrior.Sprites.Text
{
    public class Unknown : ICharSprite
    {
        public void Draw(IDrawer driver, int x, int y)
        {
            driver.Draw("?", x, y);            
        }
              
    }
}
