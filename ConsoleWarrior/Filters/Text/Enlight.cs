using ConsoleWarrior.Drivers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleWarrior.Filters.Text
{
    public class Enlight : IFilter
    {
        public void Apply(IDrawer drawer)
        {
            if(drawer is DynamicFilter d)
            {
                var prevFunction = d.ForegroundFilterFunction;
                if (prevFunction != null)
                {
                    d.ForegroundFilterFunction = x => LightsUp(prevFunction(x));
                }
                else
                {
                    d.ForegroundFilterFunction = LightsUp;
                }
            }
        }

        private ConsoleColor LightsUp(ConsoleColor consoleColor)
        {
            switch (consoleColor)
            {
                case ConsoleColor.Black:return ConsoleColor.DarkGray;
                case ConsoleColor.DarkGray: return ConsoleColor.Gray;
                case ConsoleColor.Gray: return ConsoleColor.White;
                case ConsoleColor.DarkBlue: return ConsoleColor.Blue;
                case ConsoleColor.DarkCyan: return ConsoleColor.Cyan;
                case ConsoleColor.DarkGreen: return ConsoleColor.Green;
                case ConsoleColor.DarkMagenta: return ConsoleColor.Magenta;
                case ConsoleColor.DarkRed: return ConsoleColor.Red;
                case ConsoleColor.DarkYellow: return ConsoleColor.Yellow;                
            }
            return consoleColor;
        }
    }
}
