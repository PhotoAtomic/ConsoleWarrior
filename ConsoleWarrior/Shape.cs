using System;
using System.Collections.Generic;

namespace ConsoleWarrior
{
    public abstract class Shape
    {
        public static readonly Shape Dot = new DotShape();

        internal Movement Move(World world, Entity entity, int x, int y)
        {
            IEnumerable<HashSet<Entity>> involvedCells = MarkCells(world, x, y);

            return new Movement(entity, involvedCells);
            
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
}