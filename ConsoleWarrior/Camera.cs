using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleWarrior
{
    public class Camera : Entity
    {
        HashSet<Entity> inView = new HashSet<Entity>();

        public Camera()
        {
            Console.CursorVisible = false;
            shape = new AreaShape(100, 100);

        }


        protected internal override void EnterCollision(Entity other)
        {
            inView.Add(other);
        }
        protected internal override void ExitCollision(Entity other)
        {
            inView.Remove(other);
        }

        internal void Render()
        {
            foreach (var entity in inView.OfType<IVisible>().OrderBy(x=>x.Depth))
            {
                entity.Render();
            }
        }
    }
}
