using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleWarrior
{
    public interface IAnimation
    {
        ISprite GetSprite(double elapsed);
        void Reset();
    }
}
