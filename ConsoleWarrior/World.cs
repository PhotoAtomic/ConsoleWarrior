using System;
using System.Collections.Generic;

namespace ConsoleWarrior
{
    public class World
    {
        public int Width { get; internal set; }
        public int Height { get; internal set; }

        private HashSet<Entity>[,] cells = null;

        public World(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            cells = new HashSet<Entity>[Width, Height];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    cells[x,y] = new HashSet<Entity>();
                }
            }
        }


        public HashSet<Entity> GetCell(int x, int y)
        {
            if (x < 0 || x > Width || y < 0 || x > Height) return null;
            return cells[x, y];
        }
      
    }
}
