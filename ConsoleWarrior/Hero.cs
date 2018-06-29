using System;

namespace ConsoleWarrior
{
    internal class Hero : Character, IVisible
    {
 
        public Hero()
        {
        }

        public int Depth => 0;

        public void Fire()
        {

        }

        public void Render()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write("X");
        }


    }
}