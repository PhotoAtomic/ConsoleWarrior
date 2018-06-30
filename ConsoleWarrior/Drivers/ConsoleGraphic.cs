using ConsoleWarrior.Sprites.Text;
using ConsoleWarrior.Sprites.Text.Flame;
using System;
using ConsoleWarrior.Animations;

namespace ConsoleWarrior.Drivers
{
    public class ConsoleGraphic : IDriver, IDrawer
    {


        private class Cleaner : IDrawer
        {
            public Cleaner(int width, int height)
            {
                Width = width;
                Heigth = height;
            }

            public int Width { get; }
            public int Heigth { get; }

            public void Draw(string text, int x, int y)
            {
                if (x < 0 || x >= Width || y < 0 || y >= Heigth) return;
                Console.SetCursorPosition(x, y);
                Console.WriteLine(" ");
            }
        }

        readonly int Width = 100;//Console.LargestWindowWidth;
        readonly int Heigth = 50;//Console.LargestWindowHeight;


        static readonly ISprite Unknown = new Generic("?");

        private Cleaner CleanerRenderer;

        public ConsoleGraphic()
        {
            Console.SetBufferSize(Width, Heigth);
            Console.SetWindowSize(Width, Heigth+1);
            Console.CursorVisible = false;
            CleanerRenderer = new Cleaner(Width, Heigth);
        }

        public ISprite GetSprite(string name)
        {
            switch (name)
            {
                case "Flame1": return new Flame1();
                case "Flame2": return new Flame2();
                case "Flame3": return new Flame3();
                case "Flame4": return new Flame4();
            }
            return Unknown;
        }

        public void Draw(string text, int x, int y)
        {
            
            if (x < 0 || x >= Width || y < 0 || y >= Heigth) return;
            Console.SetCursorPosition(x, y);
            Console.WriteLine(text);
        }

        public void DrawSprite(DrawRequest request)
        {

            if (request?.sprite is ICharSprite s)
            {
                request.TranslateTransform(Width / 2, Heigth / 2, 0);
                s.Draw(this, (int)request.X, (int)request.Y);
            }
        }

        public void Clean(DrawRequest request)
        {
            if (request?.sprite is ICharSprite s)
            {
                s.Draw(CleanerRenderer, (int)request.X, (int)request.Y);
            }
        }

        public Fire GetAnimation(string name)
        {
            switch (name)
            {
                case "fire":return new Fire(this);
            }
            throw new ArgumentOutOfRangeException();
        }
    }
}