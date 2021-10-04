using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    public class StarPixel
    {
        public float POSITIONX { get; set; }
        public float POSITIONY { get; set; }

        public StarPixel(int x, int y)
        {
            POSITIONX = x;
            POSITIONY = y;
        }

        public void Draw(Color c)
        {
            //Raylib.DrawPixel(POSITIONX, POSITIONY, Color.BLACK);
            Raylib.DrawPixelV(new System.Numerics.Vector2(POSITIONX, POSITIONY), c);
        }

    }
}
