using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    public enum FieldInput
    {
        Empty,
        PlayerOne,
        PlayerTwo
    }

    public class FieldEntity
    {
        public int COL_INDEX { get; set; } = 0;
        public int ROW_INDEX { get; set; } = 0;

        public int POSX { get; set; }
        public int POSY { get; set; }
        public int WIDTH { get; set; }
        public int HEIGHT { get; set; }
        public Color COLOR { get; set; }
        public bool ISSELECTED { get; set; } = false;
        public bool ISCLICKED { get; set; } = false;
        public FieldInput OCCUPATION = FieldInput.Empty;

        public FieldEntity(int x, int y, int w, int h, int col, int row)
        {
            POSX = x;
            POSY = y;
            WIDTH = w;
            HEIGHT = h;
            COL_INDEX = col;
            ROW_INDEX = row;
            COLOR = Color.WHITE;
        }

        public void Select()
        {
            ISSELECTED = true;
            COLOR = Color.LIGHTGRAY;
        }

        public void Unselect()
        {
            ISSELECTED = false;
            COLOR = Color.WHITE;
        }

        public void CLICK(FieldInput fi)
        {
            ISCLICKED = true;
            ISSELECTED = false;
            OCCUPATION = fi;

            // ==> ADD CLICK SOUND ====> Pseudo: Raylib.PlaySound("CLICK_SOUND.wav");

            if(OCCUPATION == FieldInput.PlayerOne)
            {
                COLOR = Color.RED;
            }

            if(OCCUPATION == FieldInput.PlayerTwo)
            {
                COLOR = Color.BLUE;
            }

        }


        public bool IsMouseInside(int mx, int my)
        {
            if(mx > POSX && mx < POSX + WIDTH && my > POSY && my < POSY+HEIGHT)
            {
                ISSELECTED = true;
                return true;
            }

            ISSELECTED = false;
            return false;
        }

        public void Draw()
        {
            //Raylib.DrawRectangleLines(POSX, POSY, WIDTH, HEIGHT, COLOR);
            //Raylib.DrawRectangleLines(POSX+1, POSY+1, WIDTH-2, HEIGHT-2, COLOR);

            Raylib.DrawRectangle(POSX, POSY, WIDTH, HEIGHT, COLOR);

            if (ISCLICKED == true)
            {
                if (OCCUPATION == FieldInput.PlayerOne)
                {
                    Raylib.DrawText("X", POSX+20, POSY+4, 100, Color.BLACK);
                }
                if (OCCUPATION == FieldInput.PlayerTwo)
                {
                    Raylib.DrawText("O", POSX+20, POSY+4, 100, Color.BLACK);
                }
            }
        }
    }
}
