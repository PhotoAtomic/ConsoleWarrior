using System;
using System.Collections.Generic;

namespace ConsoleWarrior
{
    public abstract class Character : Entity, IVisible
    {
        protected readonly IDriver driver;
        protected ISprite currentSprite;
        private DrawRequest previousRequest;
        private HashSet<IFilter> filters = new HashSet<IFilter>();

        public int Depth => 0;

        public Character(IDriver driver)
        {
            this.driver = driver;
        }

        protected override Movement Move(int x, int y)
        {
            return base.Move(x , y)
                .CompleteWith(Clean)
                .Commit();
        }



        public virtual Movement Left()
        {
            return Move(X - 1, Y);
        }              
        public virtual Movement Right()
        {
            return Move(X + 1, Y);
        }
        public virtual Movement Up()
        {
            return Move(X, Y - 1);
        }
        public virtual Movement Down()
        {
            return Move(X, Y + 1);
        }

        private void Clean()
        {
            driver.Clean(previousRequest);
        }

        public virtual IEnumerable<DrawRequest> Render(double elapsed)
        {
            previousRequest = new DrawRequest(currentSprite, X, Y, Depth) { Filters = filters};
            yield return previousRequest;
        }

        public void AddFilter(IFilter filter)
        {
            filters.Add(filter);
        }

        public void RemoveFilter(IFilter filter)
        {
            filters.Remove(filter);
        }
    }
}