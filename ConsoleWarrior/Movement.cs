using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleWarrior
{
    
    public class Movement :  IDisposable
    {

       

        private Entity entity;
        private IEnumerable<HashSet<Entity>> involvedCells;

        
        public IEnumerable<Entity> Vetoed { get; private set; }

        private ConcurrentQueue<Action> continuationActions = new ConcurrentQueue<Action>();
        private ConcurrentQueue<Action> completeActions = new ConcurrentQueue<Action>();
        private ConcurrentQueue<Action> rollbackActions = new ConcurrentQueue<Action>();

        private bool executed = false;
        private bool rollbacked = false;
        private bool completed = false;
        

        public bool? Success { get; private set; } = null;

        protected Movement Execute(Action nextAction)
        {
            var continuator = ContinueWith(nextAction);
            Execute();
            return continuator;
        }


        protected void Execute()
        {
            while(continuationActions.TryDequeue(out var nextAction)){
                nextAction();
            }
            executed = true;
        }


        protected void Undo()
        {
            while (rollbackActions.TryDequeue(out var rollbackAction))
            {
                rollbackAction();
            }
            rollbacked = true;
        }


        public Movement ContinueWith(Action nextAction)
        {
            if (executed)
            {
                nextAction();
            }
            else
            {
                continuationActions.Enqueue(nextAction);
            }
            return this;
        }

        public Movement RollbackWith(Action rollbackAction)
        {
            if (rollbacked)
            {
                rollbackAction();
            }
            else
            {
                rollbackActions.Enqueue(rollbackAction);
            }
            return this;
        }

        public Movement CompleteWith(Action completeAction)
        {
            if (rollbacked)
            {
                completeAction();
            }
            else
            {
                completeActions.Enqueue(completeAction);
            }
            return this;
        }

        internal Movement Commit()
        {
            var otherEntities = involvedCells.Where(x=>x!=null).SelectMany(x => x).Distinct();
            Vetoed = otherEntities.Where(x=>x.VetoCollision(this));
            if (Vetoed.Any())
            {
                Success = false;
                return this;
            }

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

            Success = true;
            return this;
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
            Undo();
        }


        internal void Complete()
        {
            while (completeActions.TryDequeue(out var completeAction))
            {
                completeAction();
            }
            completed = true;
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