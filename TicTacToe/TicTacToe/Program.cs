using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

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

        static Random rnd;
        static List<StarPixel> starsfield = new List<StarPixel>();
        static List<StarPixel> starsfield_Front = new List<StarPixel>();

        static void InitPlayboard()
        {
            rnd = new Random();

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


            int starCount = rnd.Next(500, 1000);
            for (int s = 0; s < starCount; s++)
            {
                StarPixel newStar = new StarPixel(rnd.Next(0, 300), rnd.Next(0, 300));
                starsfield.Add(newStar);
            }

            for (int s = 0; s < starCount; s++)
            {
                StarPixel newStar = new StarPixel(rnd.Next(0, 300), rnd.Next(0, 300));
                starsfield_Front.Add(newStar);
            }


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
            CurrentGameState = GameState.Ready;
        }

       

        public static void GameReady()
        {
            int indx = 0;
            foreach (var star in starsfield.ToList())
            {
                star.Draw(Color.BLACK);
                star.POSITIONX += 0.05f;
                if (star.POSITIONX >= 301)
                {
                    star.POSITIONX = -5;
                }
            }

            foreach (var star in starsfield_Front.ToList())
            {
                star.Draw(Color.LIGHTGRAY);
                star.POSITIONX += 0.02f;
                if (star.POSITIONX >= 301)
                {
                    star.POSITIONX = -5;
                }
            }


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
            bool playerOneWon = false;
            bool playerTwoWon = false;
            bool isDraw = false;
            // check if any line on the board is occupied by one player only
            if(IsPlayerOne == true)
            {
                // check for occupation == FieldInput.PlayerOne

                // check all columns
                bool isColumnWinner = true;
                for(int x = 0; x < 3; x++)
                {
                    for(int y = 0; y < 3; y++)
                    {
                        if (playboardEntities.Where(item => item.COL_INDEX == x && item.ROW_INDEX == y).FirstOrDefault().OCCUPATION != FieldInput.PlayerOne)
                        {
                            isColumnWinner = false;
                            x = 3;
                            break;
                        }
                            
                    }
                }

                // check all rows
                bool isRowWinner = true;
                for(int y = 0; y < 3; y++)
                {
                    for(int x = 0; x < 3; x++)
                    {
                        if (playboardEntities.Where(item => item.COL_INDEX == x && item.ROW_INDEX == y).FirstOrDefault().OCCUPATION != FieldInput.PlayerOne)
                        {
                            isRowWinner = false;
                            y = 3;
                            break;
                        }
                    }
                }

                // check both diagonal
                bool isDiagonalWinner = true;



                
            }
            else
            {
                // check for occupation == FieldInput.PlayerTwo
            }

            if(playboardEntities.Where(item => item.OCCUPATION == FieldInput.Empty).Count() <= 0)
            {
                // is draw
                isDraw = true;
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
