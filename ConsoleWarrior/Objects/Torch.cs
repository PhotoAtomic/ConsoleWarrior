using ConsoleWarrior.Animations;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleWarrior.Objects
{
    public class Torch : Character
    {
        IFilter enlight;
        IAnimation fire;
        public Torch(IDriver driver) : base(driver)
        {
            enlight = driver.GetFilter("enlight");
            fire = driver.GetAnimation("fire");
            shape = new AreaShape(-1, -1, 3, 3);
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
        protected internal override void EnterCollision(Entity other)
        {
            if (other is IVisible v)
            {
                v.AddFilter(enlight);
            }


        }
        protected internal override void ExitCollision(Entity other)
        {
            if (other is IVisible v)
            {
                v.RemoveFilter(enlight);
            }
        }

        

    }
}
