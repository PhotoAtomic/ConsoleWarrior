namespace ConsoleWarrior
{
    public interface IDriver
    {
        ISprite GetSprite(string spriteId);
        
        void DrawSprite(DrawRequest request);
        void Clean(DrawRequest previousRequest);
    }
}