namespace ConsoleWarrior
{
    internal interface IVisible
    {
        int Depth { get; }

        void Render();
    }
}