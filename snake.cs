using beckham_trevor_a3;
using Raylib_cs;
using System.Numerics;

public class Snake
{
    public Vector2 snakePosition;
    public int snakeRadius;
    public int snakeSize;
    public List<Vector2> snakePositionList;
    Pellets pellet = new Pellets();
    public bool snakeIsAlive = true;
    public float startingSpeed = 10f;
    public float speedIncrease = .01f;
    public float snakeSpeed;
    Direction currentDirection = Direction.Up;
    static GameAudio gameAudio = new GameAudio();


    public Snake()
    {
        snakePosition = new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight()) / 2;
        snakePositionList = new List<Vector2>();
        snakeSize = 1;
        snakeRadius = 20;




    }

    //After getting the list figured out, I kept having an issue of the snake not showing up until the player moved. So I included this function so the game draws the starting position when the game starts
    //This likely isn't an issue after I switched movement types, but if it ain't broke...
    public void SnakeStartup()
    {
        snakePositionList.Add(snakePosition);
    }

    //This function grew much larger then expected, tho I'm happy with how it works. A big part in the size was the adustment of movement style, and the many trouble-shooting steps it took to make the switch work
    public void SnakeManager()
    {
        SnakeCollision();
        SnakeDrawing();
        snakeSpeed = startingSpeed * (speedIncrease * Program.score);
        switch (currentDirection)
        {
            case Direction.Up:
                snakePosition.Y -= 1 + snakeSpeed;
                break;

            case Direction.Down:
                snakePosition.Y += 1 + snakeSpeed;
                break;

            case Direction.Right:
                snakePosition.X += 1 + snakeSpeed;
                break;

            case Direction.Left:
                snakePosition.X -= 1 + snakeSpeed;
                break;
        }

        if (snakeIsAlive == true)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.W) || currentDirection == Direction.Up)
            {
                currentDirection = Direction.Up;
                GrowthManager();

            }

            if (Raylib.IsKeyPressed(KeyboardKey.S) || currentDirection == Direction.Down)
            {
                currentDirection = Direction.Down;
                GrowthManager();
            }
            if (Raylib.IsKeyPressed(KeyboardKey.D) || currentDirection == Direction.Right)
            {

                currentDirection = Direction.Right;
                GrowthManager();
            }
            if (Raylib.IsKeyPressed(KeyboardKey.A) || currentDirection == Direction.Left)
            {

                currentDirection = Direction.Left;
                GrowthManager();
            }

        }
    }

    //This may have been the hardest part of the process, even after discussing with Raph on using an array or list, it was a struggle figuring out how to get it to update in sequence rather then all at once.
    //This was one of the problems that I was able to Frankenstien a solution based on troubleshooting and online research
    public void SnakeDrawing()
    {
        foreach (Vector2 position in snakePositionList)
        {
            Raylib.DrawCircleV(position, snakeRadius, Color.SkyBlue);

        }
    }

    public void GrowthManager()
    {
        snakePositionList.Add(new Vector2(snakePosition.X, snakePosition.Y));

        while (snakePositionList.Count > snakeSize)
        {
            snakePositionList.RemoveAt(0);
        }


    }

    //This was both easier then expected and harder than I thought. The collision setup was pretty effortless especially after the in class demo
    //The real problem came with how I'm potraying the snake, since circles overlap, technically there is ALWAYS collision, so it was a lot of trial and error to determine how to calcuate it
    //The solution was to make it so it ignores the last x amount of entiries in the list, though it still took some playtesting to find a sweetspot number as sharp turns were a hurdle
    public void SnakeCollision()
    {


        if (Program.score >= 4)
        {
            for (int i = 0; i < snakePositionList.Count - 30; i++)
            {
                float distance = Vector2.Distance(snakePosition, snakePositionList[i]) * 2;
                float radii = snakeRadius + snakeRadius;

                if (distance <= radii)
                {

                    GameOver();


                }
            }
        }

        float playerLeftSide = snakePosition.X;
        float playerRightSide = snakePosition.X + snakeRadius;
        float playerTopSide = snakePosition.Y;
        float playerBottomSide = snakePosition.Y + snakeRadius;

        if (playerLeftSide <= 40)
        {
            GameOver();
        }
        if (playerRightSide >= 770)
        {
            GameOver();
        }
        if (playerTopSide <= 30)
        {
            GameOver();
        }
        if (playerBottomSide >= 570)
        {
            GameOver();
        }
    }

    //While I am happy we are moving to 2D games beyond just text based ones, I miss how easy it was to write words to screen
    //I'm still annoyed it's not all perfectly centered >:(
    public void GameOver()
    {
        snakeIsAlive = false;
        pellet.PelletVoid();
        Raylib.DrawRectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), Color.Black);
        Raylib.DrawText($"Game Over", Raylib.GetScreenWidth() / 2 - 40, Raylib.GetScreenHeight() / 2 - 40, 20, Color.Red);
        Raylib.DrawText($"Final Score: {Program.score}", Raylib.GetScreenWidth() / 2 - 60, Raylib.GetScreenHeight() / 2 - 20, 20, Color.Red);
        Raylib.DrawText("Press \"R\" to Restart", Raylib.GetScreenWidth() / 2 - 100, Raylib.GetScreenHeight() / 2, 20, Color.Red);

        snakePositionList.Clear();
        if (Raylib.IsKeyDown(KeyboardKey.R))
        {
            snakeIsAlive = true;
            pellet.PelletReset();
            Raylib.ClearBackground(Color.RayWhite);
            Raylib.DrawRectangle(0, 0, 30, 600, Color.DarkGreen);
            Raylib.DrawRectangle(770, 0, 30, 600, Color.DarkGreen);
            Raylib.DrawRectangle(0, 0, 800, 30, Color.DarkGreen);
            Raylib.DrawRectangle(0, 570, 800, 30, Color.DarkGreen);
            Raylib.DrawText($"Score: {Program.score}", 360, 6, 20, Color.White);
            Program.score = 0;
            snakePosition = new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight()) / 2;
            snakePositionList = new List<Vector2>();
            snakeSize = 1;
            snakeRadius = 20;
            SnakeStartup();
        }


    }

    //This was orignally part of a solution suggestion online, and while I was not quite sure why, I re-read our enum module and thought it was a helpful way to organize the directions 
    //Especially as I kept getting Y-, Y+, etc mixed up
    enum Direction
    {
        Up,
        Down,
        Left,
        Right,
    }
}


