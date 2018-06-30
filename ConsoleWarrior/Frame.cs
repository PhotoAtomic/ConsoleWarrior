namespace ConsoleWarrior
{
    internal class Frame
    {
        public Frame(ISprite sprite, double timePoint)
        {
            Sprite = sprite;
            TimePoint = timePoint;
        }

        public ISprite Sprite { get; }
        public double TimePoint { get; }
    }
}