using System.Numerics;
using MavisVsTheDevil;
using MavisVsTheDevil.Engine;
using Raylib_cs;
using static Raylib_cs.Raylib;

public static class Program
{
    public static Random random = new Random();
    private static Game _game = new Game();
    public static GameWindow GameWindow => _window;
    private static GameWindow _window;
    public const int GLSL_VERSION = 330;
    public static Font terminalFont;
	public static int Main()
    {
        const int screenWidth = 1920;
        const int screenHeight = 1080;
        SetConfigFlags(ConfigFlags.ResizableWindow);

        InitWindow(screenWidth, screenHeight, "Mavis Vs. The Devil");
        _window = new GameWindow(_game);
        //Load Resources
        terminalFont = LoadFont("Resources/terminal F4.ttf");

        SetTargetFPS(144);

        _game.StartGame();
        while (!WindowShouldClose())
        {
	        if (IsWindowResized())
	        {
		        Resize();
	        }
	        
            // Update loop
            _game.Tick();
            
            // Draw
            BeginDrawing();
	            ClearBackground(Color.Black);
	            _window.Draw();
            EndDrawing();
        }

        
        //Unload Resources
		_window.OnClose();
        UnloadFont(terminalFont);
        
        CloseWindow();

        return 0;
    }

	private static void Resize()
	{
		_window.SetSizes();
	}
}