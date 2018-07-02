using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleWarrior
{

    public class Movement : IDisposable
    {



        public readonly Entity Entity;
        private IEnumerable<HashSet<Entity>> involvedCells;
        private IEnumerable<Entity> alreadyInvolvedEntitites;

        public IEnumerable<Entity> ImpactedEntities { get; private set; }

        public IEnumerable<Entity> Vetoed { get; private set; }

        private ConcurrentQueue<Action> continuationActions = new ConcurrentQueue<Action>();
        private ConcurrentQueue<Action> completeActions = new ConcurrentQueue<Action>();
        private ConcurrentQueue<Action> rollbackActions = new ConcurrentQueue<Action>();

        private bool executed = false;
        private bool rollbacked = false;
        private bool completed = false;
        internal readonly int TargetX;
        internal readonly int TargetY;

        public bool? Success { get; private set; } = null;

        protected Movement Execute(Action nextAction)
        {
            var continuator = ContinueWith(nextAction);
            Execute();
            return continuator;
        }


        protected void Execute()
        {
            while (continuationActions.TryDequeue(out var nextAction)) {
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
            if (completed)
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
            ImpactedEntities = involvedCells.Where(x => x != null).SelectMany(x => x).Distinct();
            if (alreadyInvolvedEntitites != null) ImpactedEntities = ImpactedEntities.Except(alreadyInvolvedEntitites);
            Vetoed = ImpactedEntities.Where(x => x.VetoCollision(this));
            if (Vetoed.Any())
            {
                Success = false;
                return this;
            }

            Execute(() =>
            {
                foreach (var cell in involvedCells.Where(x => !x.Contains(Entity)))
                {
                    cell.Add(Entity);
                }
                foreach (var other in ImpactedEntities)
                {
                    Entity.EnterCollision(other);
                    other.EnterCollision(Entity);
                }
            });

            Success = true;
            return this;
        }

        internal void Rollback()
        {
            ExitCells();
            Undo();
        }

        private void ExitCells()
        {
            var otherEntities = involvedCells
                            .Where(x => x != null && x.Contains(Entity))
                            .SelectMany(x => x)
                            .Distinct();

            if (alreadyInvolvedEntitites != null) otherEntities = alreadyInvolvedEntitites.Except(otherEntities);

            foreach (var otherEntity in otherEntities)
            {
                Entity.ExitCollision(otherEntity);
                otherEntity.ExitCollision(Entity);
            }

            foreach (var involvedCell in involvedCells)
            {
                involvedCell.Remove(Entity);
            }
        }

        internal void Complete()
        {
            ExitCells();
            Finalize();
        }

        private void Finalize()
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

        public Movement ConsiderAlreadyInvolved(IEnumerable<Entity> alreadyInvolvedEntities)
        {
            this.alreadyInvolvedEntitites = alreadyInvolvedEntities;
            return this;
        }

        public Movement(Entity entity, int targetX, int targetY, IEnumerable<HashSet<Entity>> involvedCells)
        {
            this.TargetX = targetX;
            this.TargetY = targetY;
            this.Entity = entity;
            this.involvedCells = involvedCells;         
            
        }

             
    }
}