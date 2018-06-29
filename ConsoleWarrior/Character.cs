using System;

namespace ConsoleWarrior
{
    internal class Character : Entity
    {        
        public virtual void Left()
        {
            var prevX = X;
            var prevY = Y;

            Move(X - 1, Y)                
                .Commit();
        }


        public virtual void Right()
        {
            Move(X + 1, Y)
                .Commit();
        }
        public virtual void Up()
        {
            Move(X , Y - 1)
                .Commit();
        }
        public virtual void Down()
        {
            Move(X , Y + 1)
                .Commit();
        }
    }
}