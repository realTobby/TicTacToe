using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    public class FieldEntity
    {
        public int POSX { get; set; }
        public int POSY { get; set; }

        public int WIDTH { get; set; }
        public int HEIGHT { get; set; }
        public Color COLOR { get; set; }
        public bool ISSELECTED { get; set; } = false;
        public bool ISCLICKED { get; set; } = false;
        public Color SELECTED_COLOR = Color.YELLOW;
        public Color CLICKED_COLOR = Color.RED;
        public Color NORMAL_COLOR = Color.BLACK;

        public FieldEntity(int x, int y, int w, int h)
        {
            POSX = x;
            POSY = y;
            WIDTH = w;
            HEIGHT = h;
            COLOR = Color.BLACK;
        }

        public void Select()
        {
            ISSELECTED = true;
            COLOR = SELECTED_COLOR;
        }

        public void Unselect()
        {
            ISSELECTED = false;
            COLOR = NORMAL_COLOR;
        }

        public void CLICK()
        {
            ISCLICKED = !ISCLICKED;
            if(ISCLICKED == true)
            {
                COLOR = CLICKED_COLOR;
                ISSELECTED = false;
            }

            if(ISCLICKED == false)
            {
                COLOR = NORMAL_COLOR;
                ISCLICKED = false;
                ISSELECTED = false;
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
            Raylib.DrawRectangle(POSX, POSY, WIDTH, HEIGHT, COLOR);
        }

    }
}
