using ConsoleWarrior.Drivers;

namespace ConsoleWarrior
{
    public interface IFilter
    {
        void Apply(IDrawer drawer);
    }
}