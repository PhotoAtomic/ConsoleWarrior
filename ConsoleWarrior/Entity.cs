using System;

namespace ConsoleWarrior
{
    public class Entity: IDisposable
    {
        private Movement previousMovement = null;
        private World world;
        protected Shape shape = new DotShape();

        public int X { get; private set; }
        public int Y { get; private set; }

        internal Movement Attach(World world, int x, int y)
        {
            this.world = world;
            return Move(x, y);
        }

        protected virtual Movement Move(int x, int y)
        {
            previousMovement = shape.Move(world, this, x, y)
                .ConsiderAlreadyInvolved(previousMovement?.ImpactedEntities)
                .ContinueWith(() => previousMovement?.Complete())
                .ContinueWith(() => { X = x; Y = y; });
                
            return previousMovement;
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
