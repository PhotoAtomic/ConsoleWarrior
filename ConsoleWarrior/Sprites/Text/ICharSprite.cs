using ConsoleWarrior.Drivers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleWarrior.Sprites.Text
{
    public interface ICharSprite : ISprite
    {
        void Draw(IDrawer driver, int x, int y);
    }
}
