﻿using System;
using System.Collections.Generic;

namespace ConsoleWarrior
{
    public class DrawRequest
    {
        public ISprite sprite;
        public float X, Y, Z;
        
        

        public DrawRequest(ISprite sprite, float x, float y, float z) 
        {
            this.sprite = sprite;
            X = x;
            Y = y;
            Z = z;
        }

        public IEnumerable<IFilter> Filters { get; set; }

        internal void TranslateTransform(int x, int y, int z)
        {
            X += x;
            Y += y;
            Z += z;
        }
    }
}