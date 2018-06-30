using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleWarrior
{
    public class CycleAnimation : IAnimation
    {
        private List<Frame> frames = new List<Frame>();

        private double total;
        private double cumulative;
        
        protected void AddFrame(ISprite sprite, long lasts)
        {
            total += lasts;
            frames.Add(new Frame(sprite, total));
            
        }

        public ISprite GetSprite(double elapsed)
        {
            cumulative += elapsed;
            cumulative = cumulative % total;
            return frames.Where(x => x.TimePoint > cumulative).First()?.Sprite;
        }

        public void Reset()
        {
            cumulative = 0;
        }
    }
}
