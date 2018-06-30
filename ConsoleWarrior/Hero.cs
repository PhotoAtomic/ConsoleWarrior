using System;
using System.Collections.Generic;

namespace ConsoleWarrior
{
    internal class Hero : Character
    {
        private readonly ISprite idle;
        public Hero(IDriver driver):base(driver)
        {
            idle = driver.GetSprite("Hero-Idle");
            currentSprite = idle;
        }
        
        public void Fire()
        {

        }

        
    }
}