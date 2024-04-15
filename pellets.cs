using beckham_trevor_a3;
using Raylib_cs;
using System.Numerics;


public class Pellets
{

    int remainingPellets;
    int pelletMapY;
    int pelletMapX;
    float pelletRadius;
    Color[] pelletColor = new Color[1000];
    Vector2[] pelletLocation = new Vector2[1000]; //Originally had these set to 10,000 but after adding the speed adjustments, figure it's unikely someone will get that high. Doubt someone will get to 1k either but keepin it just in case :P
    static Program program = new Program();
    static Snake snake = new Snake();
    public Pellets()
    {
        PelletReset();
    }


    public void PelletSpawner()
    {
        if (remainingPellets != 0)
        {
            Raylib.DrawCircleV(pelletLocation[remainingPellets], pelletRadius, pelletColor[remainingPellets]);
            remainingPellets--;
        }
    }

    public void PelletCollection(Snake snake)
    {

        float distance = Vector2.Distance(snake.snakePosition, pelletLocation[remainingPellets]);
        float radii = snake.snakeRadius + pelletRadius;

        if (distance < radii)
        {
            pelletLocation[remainingPellets] = Vector2.Zero;
            pelletColor[remainingPellets] = Color.DarkGreen;
            Program.score++;
            snake.snakeSize += 5;
            PelletSpawner();
        }

        else if (snake.snakeIsAlive == true)
        {
            Raylib.DrawCircleV(pelletLocation[remainingPellets], pelletRadius, Color.Pink);

        }

    }

    //This is one that is kinda redundent, but I decided to keep it just in case. I created this because I kept having the pellet stay in the same spot after reset
    //But after focusing on it for like 2 hours I said to hell with it and moved on. 
    public void PelletVoid()
    {
        for (int i = 0; i < pelletLocation.Length; i++)
        {
            pelletLocation[i] = Vector2.Zero;
            pelletColor[i] = Color.Black;
        }
    }

    public void PelletReset()
    {
        remainingPellets = 999;
        pelletRadius = 7.5f;
        for (int i = 0; i < pelletLocation.Length; i++)
        {
            Random rng = new Random();
            pelletMapY = rng.Next(50, 550);
            pelletMapX = rng.Next(50, 750);
            pelletLocation[i] = new Vector2(pelletMapX, pelletMapY);
            pelletColor[i] = Color.Pink;
        }
    }
}


