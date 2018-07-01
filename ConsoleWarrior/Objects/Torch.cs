using ConsoleWarrior.Animations;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleWarrior.Objects
{
    public class Torch : Character
    {
        Fire fire;
        public Torch(IDriver driver) : base(driver)
        {
            fire = driver.GetAnimation("fire");
        }
        public override IEnumerable<DrawRequest> Render(double elapsed)
        {
            currentSprite = fire.GetSprite(elapsed);
            return base.Render(elapsed);
        }
        protected internal override bool VetoCollision(Movement movement)
        {
            return (movement.Entity is Character) && 
                movement.TargetX == this.X &&
                movement.TargetY == this.Y;
            
        }
    }
}
