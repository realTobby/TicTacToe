using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
    class Program
    {
        static List<FieldEntity> playboardEntities = new List<FieldEntity>();

        static bool IsPlayerOne = true;

        static int Click_Counter = 0;

        static void InitPlayboard()
        {
            playboardEntities.Clear();
            playboardEntities = new List<FieldEntity>();

            playboardEntities.Add(new FieldEntity(0, 0, 75, 75,0,0));
            playboardEntities.Add(new FieldEntity(100, 0, 75, 75,1,0));
            playboardEntities.Add(new FieldEntity(200, 0, 75, 75,2,0));

            playboardEntities.Add(new FieldEntity(0, 100, 75, 75, 0,1));
            playboardEntities.Add(new FieldEntity(100, 100, 75, 75, 1,1));
            playboardEntities.Add(new FieldEntity(200, 100, 75, 75, 2,1));

            playboardEntities.Add(new FieldEntity(0, 200, 75, 75, 0,2));
            playboardEntities.Add(new FieldEntity(100, 200, 75, 75, 1,2));
            playboardEntities.Add(new FieldEntity(200, 200, 75, 75, 2,2));


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

                FieldInput fiReady = FieldInput.Empty;

                if (IsPlayerOne == true)
                    fiReady = FieldInput.PlayerOne;
                else
                    fiReady = FieldInput.PlayerTwo;

                //DebugShowMousePosition();
                DrawPlayboard();
                CheckMouseOverField();

                if(Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                {
                    var clickedField = playboardEntities.Where(item => item.ISSELECTED == true).FirstOrDefault();
                    if(clickedField != null)
                    {
                        if(clickedField.ISCLICKED == false)
                        {
                            clickedField.CLICK(fiReady);
                            IsPlayerOne = !IsPlayerOne;
                            Click_Counter++;
                            if(Click_Counter >= 9)
                            {
                                Reset();
                            }
                            CheckForWin();
                        }
                        
                    }
                }
                
                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }

        public static void Reset()
        {
            InitPlayboard();
            IsPlayerOne = true;
            Click_Counter = 0;
        }

        private static void CheckForWin()
        {
            // check if any line on the board is occupied by one player only





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
