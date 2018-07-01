using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleWarrior
{
    public abstract class Shape
    {
        public static readonly Shape Dot = new DotShape();

        internal Movement Move(World world, Entity entity, int x, int y)
        {
            IEnumerable<HashSet<Entity>> involvedCells = MarkCells(world, x, y).Where(cell => cell!=null).ToArray();

            return new Movement(entity,x,y, involvedCells);
            
        }

        protected abstract IEnumerable<HashSet<Entity>> MarkCells(World world, int x, int y);
    }

    public class DotShape : Shape
    {
        protected override IEnumerable<HashSet<Entity>> MarkCells(World world, int x, int y)
        {
            yield return world.GetCell(x, y);
        }
    }

    public class AreaShape : Shape
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public AreaShape(int width, int height)
        {
            Width = width;
            Height = height;
        }

        protected override IEnumerable<HashSet<Entity>> MarkCells(World world, int x, int y)
        {
            for( ; x<Width; x++)
            {
                for (; y < Height; y++)
                {
                    yield return world.GetCell(x, y);
                }
            }
            
        }
    }
}