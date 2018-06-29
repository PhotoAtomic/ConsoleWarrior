using System;

namespace ConsoleWarrior
{
    public class Entity: IDisposable
    {
        private Movement previousMovement = null;
        private World world;
        protected Shape shape = Shape.Dot;

        public int X { get; private set; }
        public int Y { get; private set; }

        internal Movement Attach(World world, int x, int y)
        {
            this.world = world;
            return Move(x, y);
        }

        protected Movement Move(int x, int y)
        {
            Movement movement = shape.Move(world, this, x, y);
            movement
                .ContinueWith(() => previousMovement?.Rollback())
                .ContinueWith(()=> { X = x; Y = y; });
            return movement;
        }

        protected internal virtual bool VetoCollision(Movement movement)
        {
            return false;
        }

        protected internal virtual void EnterCollision(Entity other)
        {        
        }

        protected internal virtual void ExitCollision(Entity other)
        {
            
        }

        public void Dispose()
        {
            previousMovement?.Dispose();
        }
    }
}
