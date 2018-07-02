using ConsoleWarrior.Animations;

namespace ConsoleWarrior
{
    public interface IDriver
    {
        ISprite GetSprite(string name);
        
        void DrawSprite(DrawRequest request);
        void Clean(DrawRequest previousRequest);
        IAnimation GetAnimation(string name);
        IFilter GetFilter(string name);
    }
}