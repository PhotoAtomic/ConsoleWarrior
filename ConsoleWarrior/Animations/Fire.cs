using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleWarrior.Animations
{
    public class Fire : CycleAnimation
    {
        public Fire(IDriver driver)
        {
            AddFrame(driver.GetSprite("Flame1"), 200);
            AddFrame(driver.GetSprite("Flame2"), 300);
            AddFrame(driver.GetSprite("Flame3"), 100);
            AddFrame(driver.GetSprite("Flame4"), 200);
            AddFrame(driver.GetSprite("Flame2"), 200);
            AddFrame(driver.GetSprite("Flame3"), 300);
        }
    }
}
