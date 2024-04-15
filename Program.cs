using Raylib_cs;
using System.Numerics;

namespace beckham_trevor_a3
{
    public class Program
    {
        static string title = "Snake - Trevor (TJ) Beckham A3 assignment";

        public static Color wallColor = Color.Black;
        public static int score = 0;
        static Pellets pellets = new Pellets();
        static Snake snake = new Snake();
        static GameAudio gameAudio;


        static void Main(string[] args)
        {
            Raylib.InitWindow(800, 600, title);
            Raylib.SetTargetFPS(60);

            Setup();
            
            while (!Raylib.WindowShouldClose())
            {
                
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.White);

                Update();
                Raylib.EndDrawing();
            }
            Raylib.CloseWindow();
        }

        static void Setup()
        {
            snake = new Snake();
            gameAudio = new GameAudio();
            gameAudio.LoadMusic();
            gameAudio.BackgroundMusic();

            snake.SnakeStartup();
            

        }

        static void Update()
        {


            
            if (snake.snakeIsAlive == true)
            {
                Raylib.UpdateMusicStream(gameAudio.music);
                Raylib.DrawRectangle(0, 0, 30, 600, Color.Lime);
                Raylib.DrawRectangle(770, 0, 30, 600, Color.Lime);
                Raylib.DrawRectangle(0, 0, 800, 30, Color.Lime);
                Raylib.DrawRectangle(0, 570, 800, 30, Color.Lime);
                Raylib.DrawText($"Score: {score}", 360, 6, 20, Color.White);
            }

            if (snake.snakeIsAlive == false)
            {
                snake.GameOver();
            }
   
            //my original build had a few functions referenced here, but I worked to condense it a bit, unitentionally
            snake.SnakeManager();
            pellets.PelletCollection(snake);








        }
        // To continue my trend from the last assignment, below is my CodeGraveyard that includes some code that I tried to leverage for certain tasks that didn't work but I wanted to keep on hand just in case
        // And since I don't like deleting stuff, it stays! 

        //public void CodeGraveYard(Home of Misfit Code)
        //{

        //foreach (Vector2 position in snakePositionList)
        //{
        //    float distance = Vector2.Distance(snakePosition, snakePositionList);
        //    float radii = snakeRadius + snakeRadius;

        //    if (distance < radii)
        //    {
        //        Raylib.DrawRectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), Color.Black);
        //        Raylib.DrawText($"Game Over\n Final Score:{Program.score}", Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), 100, Color.White);
        //        //snakePositionList.Clear();

        //    }
        //}

        //}
    }
}