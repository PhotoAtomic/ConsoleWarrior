namespace ConsoleWarrior
{
    internal interface IController
    {
        bool Exit { get; }

        void ProcessInput();
    }
}