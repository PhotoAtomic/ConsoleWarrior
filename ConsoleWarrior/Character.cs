namespace ConsoleWarrior
{
    internal class Character : Entity
    {        
        public virtual void Left()
        {
            var mov = Move(X - 1, Y);            
            mov.Commit();
        }
        public virtual void Right()
        {
            Move(X + 1, Y);
        }
        public virtual void Up()
        {
            Move(X , Y - 1);
        }
        public virtual void Down()
        {
            Move(X , Y + 1);
        }
    }
}