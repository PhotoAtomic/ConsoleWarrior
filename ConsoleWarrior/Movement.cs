using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleWarrior
{

    public class MovementContinuation
    {
        private MovementContinuation continuation;
        private Action continuationAction;
        private bool executed = false;


        internal MovementContinuation()
        {
         
        }

        public MovementContinuation(Action continuationAction)
        {
            this.continuationAction = continuationAction;
        }

        public MovementContinuation ContinueWith(Action continuationAction)
        {
            var last = this;
            while (last.continuation != null)
            {
                last = last.continuation;
            }

            last.continuation = new MovementContinuation(continuationAction);
            if (last.executed) last.continuation.Execute();
            return last.continuation;
        }

        public void Execute(Action action)
        {
            if (continuationAction != null) throw new InvalidOperationException("Continuation action already set");
            continuationAction = action;
            Execute();
        }

        public void Execute()
        {
            executed = true;
            continuationAction?.Invoke();
            continuation?.Execute();
        }
    }

    public class Movement : MovementContinuation, IDisposable
    {

        

        private Entity entity;
        private IEnumerable<HashSet<Entity>> involvedCells;

        
        public IEnumerable<Entity> Vetoed { get; private set; }

        internal bool Commit()
        {
            var otherEntities = involvedCells.Where(x=>x!=null).SelectMany(x => x).Distinct();
            Vetoed = otherEntities.Where(x=>x.VetoCollision(this));
            if (Vetoed.Any()) return false;

            Execute(()=>
            {
                foreach (var cell in involvedCells.Where(x => !x.Contains(entity)))
                {
                    cell.Add(entity);
                }
                foreach (var other in otherEntities)
                {
                    entity.EnterCollision(other);
                    other.EnterCollision(entity);
                }
            });

            return true;
        }

        internal void Rollback()
        {
            var otherEntities = involvedCells
                .Where(x=>x != null && x.Contains(entity))
                .SelectMany(x => x)
                .Distinct();

            foreach(var otherEntity in otherEntities)
            {
                entity.ExitCollision(otherEntity);
                otherEntity.ExitCollision(entity);
            }

            foreach (var involvedCell in involvedCells)
            {
                involvedCell.Remove(entity);
            }
            
        }

        public void Dispose()
        {
            Rollback();
        }

        public Movement(Entity entity, IEnumerable<HashSet<Entity>> involvedCells)
        {
            this.entity = entity;
            this.involvedCells = involvedCells;            
        }

             
    }
}