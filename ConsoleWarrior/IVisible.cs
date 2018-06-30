using System.Collections.Generic;

namespace ConsoleWarrior
{
    internal interface IVisible
    {
        int Depth { get; }

        IEnumerable<DrawRequest> Render(double elapsed);
    }
}