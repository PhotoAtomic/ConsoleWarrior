using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleWarrior
{
    public abstract class Shape
    {
        //public static readonly Shape Dot = new DotShape();

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
        
        public int Top { get; set; }
        public int Left { get; set; }
        public int Right { get; set;  }
        public int Bottom { get; set;  }

        public AreaShape(int left, int top, int right, int bottom)
        {
            Top = top;
            Left = left;
            Right = right;
            Bottom = bottom;
            
        }

        public AreaShape(int width, int height): this(0, 0, width, height)
        {            
        }

        protected override IEnumerable<HashSet<Entity>> MarkCells(World world, int x, int y)
        {
            for( var i = Left; i<=Right; i++)
            {
                for (var j = Top; j <= Bottom; j++)
                {
                    yield return world.GetCell(i + x, j + y);
                }
            }
            
        }
    }
}