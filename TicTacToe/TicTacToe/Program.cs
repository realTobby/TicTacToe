using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
    class Program
    {
        static List<FieldEntity> playboardEntities = new List<FieldEntity>();

        static void InitPlayboard()
        {
            playboardEntities.Add(new FieldEntity(0, 0, 50, 50));
            playboardEntities.Add(new FieldEntity(100, 0, 50, 50));
            playboardEntities.Add(new FieldEntity(200, 0, 50, 50));

            playboardEntities.Add(new FieldEntity(0, 100, 50, 50));
            playboardEntities.Add(new FieldEntity(100, 100, 50, 50));
            playboardEntities.Add(new FieldEntity(200, 100, 50, 50));

            playboardEntities.Add(new FieldEntity(0, 200, 50, 50));
            playboardEntities.Add(new FieldEntity(100, 200, 50, 50));
            playboardEntities.Add(new FieldEntity(200, 200, 50, 50));


        }

        static void CheckMouseOverField()
        {
            var mousePos = Raylib.GetMousePosition();

            foreach(var fe in playboardEntities.ToList())
            {
                if(fe.IsMouseInside(Convert.ToInt32(mousePos.X), Convert.ToInt32(mousePos.Y)))
                {
                    if(fe.ISCLICKED == false)
                        fe.Select();
                }
                else
                {
                    if(fe.ISCLICKED == false)
                        fe.Unselect();
                }
            }

        }

        static void Main(string[] args)
        {
            Raylib.InitWindow(300, 300, "Tic-Tac-Toe");

            InitPlayboard();

            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.WHITE);

                DebugShowMousePosition();
                DrawPlayboard();
                CheckMouseOverField();

                if(Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                {
                    var clickedField = playboardEntities.Where(item => item.ISSELECTED == true).FirstOrDefault();
                    if(clickedField != null)
                    {
                        clickedField.CLICK();
                    }
                }
                
                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }

        private static void DebugShowMousePosition()
        {
            var pos = Raylib.GetMousePosition();

            Raylib.DrawText(pos.X + " / " + pos.Y, 50, 50, 24, Color.BLUE);
        }

        private static void DrawPlayboard()
        {
            foreach(var fe in playboardEntities)
            {
                fe.Draw();
            }
        }
    }
}
