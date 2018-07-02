using ConsoleWarrior.Sprites.Text;
using ConsoleWarrior.Sprites.Text.Flame;
using System;
using ConsoleWarrior.Animations;
using ConsoleWarrior.Filters.Text;

namespace ConsoleWarrior.Drivers
{

    public class Filter : IDrawer
    {
        public Filter(int width, int height)
        {
            Width = width;
            Heigth = height;
        }

        public int Width { get; }
        public int Heigth { get; }

        public void Draw(string text, int x, int y, ConsoleColor foreground = ConsoleColor.Gray, ConsoleColor background = ConsoleColor.Black)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Heigth) return;
            Console.BackgroundColor = FilterBackground(background);
            Console.ForegroundColor = FilterForeground(foreground);
            Console.SetCursorPosition(x, y);
            Console.WriteLine(FilterText(text));
        }

        protected virtual string FilterText(string text)
        {
            return text;
        }

        protected virtual ConsoleColor FilterForeground(ConsoleColor foreground)
        {
            return foreground;
        }

        protected virtual ConsoleColor FilterBackground(ConsoleColor background)
        {
            return background;
        }
    }

    public class Cleaner : Filter
    {
        public Cleaner(int width, int height) : base(width, height) { }
        protected override string FilterText(string text)
        {
            return " ";
        }

    }

    public class DynamicFilter : Filter
    {
        public DynamicFilter(int width, int height) : base(width, height) { }

        public Func<string, string> TextFilterFunction { get; set; }
        public Func<ConsoleColor, ConsoleColor> ForegroundFilterFunction { get; set; }

        public Func<ConsoleColor, ConsoleColor> BackgroundFilterFunction { get; set; }

        public void Reset()
        {
            ForegroundFilterFunction = null;
            BackgroundFilterFunction = null;
        }

        protected override string FilterText(string text)
        {
            return TextFilterFunction?.Invoke(text) ?? base.FilterText(text);
        }
        protected override ConsoleColor FilterBackground(ConsoleColor color)
        {
            return BackgroundFilterFunction?.Invoke(color) ?? base.FilterBackground(color);
        }
        protected override ConsoleColor FilterForeground(ConsoleColor color)
        {
            return ForegroundFilterFunction?.Invoke(color) ?? base.FilterForeground(color);
        }

    }

    public class ConsoleGraphic : IDriver
    {

        




        readonly int Width = 100;//Console.LargestWindowWidth;
        readonly int Heigth = 50;//Console.LargestWindowHeight;


        static readonly ISprite Unknown = new Generic("?");

        private Cleaner CleanerRenderer;
        private DynamicFilter DynamicRenderer;

        public ConsoleGraphic()
        {
            Console.SetWindowSize(Width, Heigth);
            Console.SetBufferSize(Width, Heigth);
            
            Console.CursorVisible = false;
            CleanerRenderer = new Cleaner(Width, Heigth);
            DynamicRenderer = new DynamicFilter(Width, Heigth);
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

        public void Draw(string text, int x, int y, ConsoleColor foreground = ConsoleColor.Gray, ConsoleColor background = ConsoleColor.Black)
        {

            if (x < 0 || x >= Width || y < 0 || y >= Heigth) return;
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;
            Console.WriteLine(text);
        }

        public void DrawSprite(DrawRequest request)
        {

            if (request?.sprite is ICharSprite s)
            {
                request.TranslateTransform(Width / 2, Heigth / 2, 0);
                DynamicRenderer.Reset();
                foreach(var filter in request.Filters)
                {
                    filter.Apply(DynamicRenderer);
                }
                s.Draw(DynamicRenderer, (int)request.X, (int)request.Y);
            }
        }

        public void Clean(DrawRequest request)
        {
            if (request?.sprite is ICharSprite s)
            {
                s.Draw(CleanerRenderer, (int)request.X, (int)request.Y);
            }
        }

        public IAnimation GetAnimation(string name)
        {
            switch (name)
            {
                case "fire": return new Fire(this);
            }
            throw new ArgumentOutOfRangeException();
        }

        public IFilter GetFilter(string name)
        {
            switch (name)
            {
                case "enlight": return new Enlight();
            }
            throw new ArgumentOutOfRangeException();
        }
    }
}    
