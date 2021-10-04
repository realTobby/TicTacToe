using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
    public enum GameState
    {
        Ready,
        Playing,
        End
    }

    class Program
    {
        static GameState CurrentGameState = GameState.Ready;

        static List<FieldEntity> playboardEntities = new List<FieldEntity>();

        static bool IsPlayerOne = true;

        static int Click_Counter = 0;

        static void InitPlayboard()
        {
            playboardEntities.Clear();
            playboardEntities = new List<FieldEntity>();

            playboardEntities.Add(new FieldEntity(0, 0, 100, 100, 0,0));
            playboardEntities.Add(new FieldEntity(100, 0, 100, 100, 1,0));
            playboardEntities.Add(new FieldEntity(200, 0, 100, 100, 2,0));

            playboardEntities.Add(new FieldEntity(0, 100, 100, 100, 0,1));
            playboardEntities.Add(new FieldEntity(100, 100, 100, 100, 1,1));
            playboardEntities.Add(new FieldEntity(200, 100, 100, 100, 2,1));

            playboardEntities.Add(new FieldEntity(0, 200, 100, 100, 0,2));
            playboardEntities.Add(new FieldEntity(100, 200, 100, 100, 1,2));
            playboardEntities.Add(new FieldEntity(200, 200, 100, 100, 2,2));


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

                switch(CurrentGameState)
                {
                    case GameState.Ready:
                        GameReady();
                        break;
                    case GameState.Playing:
                        GamePlaying();
                        break;
                    case GameState.End:
                        GameEnd();
                        break;
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
            CurrentGameState = GameState.Playing;
        }

        public static void GameReady()
        {
            Raylib.DrawText("Tic-TacToe", 0, 0, 16, Color.BLACK);
            Raylib.DrawText("Press anywhere to play!", 0, 65, 16, Color.BLACK);

            if(Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
            {
                CurrentGameState = GameState.Playing;
            }

        }

        public static void GameEnd()
        {
            DrawPlayboard();

            Raylib.DrawRectangle(0, 0, 230, 50, Color.WHITE);

            Raylib.DrawText("GameEnd!", 0, 0, 16, Color.BLACK);
            Raylib.DrawText("Press anywhere to play again!", 0, 20, 16, Color.BLACK);

            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
            {
                Reset();
            }
        }

        public static void GamePlaying()
        {
            FieldInput fiReady = FieldInput.Empty;

            if (IsPlayerOne == true)
                fiReady = FieldInput.PlayerOne;
            else
                fiReady = FieldInput.PlayerTwo;

            //DebugShowMousePosition();
            DrawPlayboard();
            CheckMouseOverField();

            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
            {
                var clickedField = playboardEntities.Where(item => item.ISSELECTED == true).FirstOrDefault();
                if (clickedField != null)
                {
                    if (clickedField.ISCLICKED == false)
                    {
                        clickedField.CLICK(fiReady);
                        CheckForWin();
                        IsPlayerOne = !IsPlayerOne;
                        Click_Counter++;
                        if (Click_Counter >= 9)
                        {
                            CurrentGameState = GameState.End;
                        }
                    }
                }
            }
        }

        private static void CheckForWin()
        {
            // check if any line on the board is occupied by one player only
            if(IsPlayerOne == true)
            {
                // check for occupation == FieldInput.PlayerOne

                
            }
            else
            {
                // check for occupation == FieldInput.PlayerTwo
            }

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
