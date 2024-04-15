using Raylib_cs;
using System.Numerics;

public class GameAudio
{
    public Music music;
    
    //While I ended up reducing scope of my original, pulsating snake vision, I wanted to make sure I still included music in some way...
    //After the two hours trying to get this to work, I'm happy I still did it... but I'd be a bit more sane if I didn't
    public GameAudio()
    {

        Raylib.InitAudioDevice();
        float volume = Raylib.GetMasterVolume();
        Raylib.SetMasterVolume(volume);
        string dir = "C:/Users/tjbec/OneDrive/Game Design/Semester 1/Game Dev fundamentla/Assignment #3/beckham_trevor_a3";
        Directory.SetCurrentDirectory(dir);

    }


    //The biggest issue I had was loading the file, as the directory the program uses wasn't the project file, but rather the bin file
    //Thankfully, a helpful prof provided code from our last project to pull details around file directories, so I just lifted and adjusted that code to create the below
    public void LoadMusic()
    {
       
        string cwd = Directory.GetCurrentDirectory();
        string dirName = Path.GetFileName(cwd);
        Console.WriteLine(cwd);
        bool isDebugBuild = dirName.Contains("net", StringComparison.Ordinal);
        if (isDebugBuild)
        {
            Directory.SetCurrentDirectory(@"..\..\..\");
            cwd = Directory.GetCurrentDirectory();
        }
        string filename = "background-music.wav";
        string filePath = Path.Combine(cwd, filename);
        music = Raylib.LoadMusicStream(filePath);
    }
    public void BackgroundMusic()
    {
        Raylib.PlayMusicStream(music);
    }

}

