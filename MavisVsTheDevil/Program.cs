using System.Numerics;
using MavisVsTheDevil;
using MavisVsTheDevil.Elements;
using MavisVsTheDevil.Engine;
using MavisVsTheDevil.Panels;
using Raylib_cs;
using static Raylib_cs.Raylib;

public unsafe static class Program
{
    public static Random random = new Random();
    private static Game _game = new Game();
    public static GameWindow GameWindow => _window;
    private static GameWindow _window;
    public const int GLSL_VERSION = 330;
    public static Font terminalFont;//exact for typing
    public static Font titleFont;//exact for title
    public static Font headerFont;//big for big

    public static bool UseShaders = true;
	public static int Main()
	{
		//UseShaders = false;//mac!
        const int screenWidth = 1920;
        const int screenHeight = 1080;
        SetConfigFlags(ConfigFlags.ResizableWindow);
        InitWindow(screenWidth, screenHeight, "Mavis Vs. The Devil");
        BeginDrawing();
	    Raylib.DrawText("Loading", 20,20,30, Color.Black);
        EndDrawing();
        _window = new GameWindow(_game);
        //Load Resources
        terminalFont = LoadFontEx("Resources/terminal.ttf", TypingWindow.FontHeight, null,0);
        SetTextureFilter(terminalFont.Texture, TextureFilter.Point);
        headerFont = LoadFontEx("Resources/terminal.ttf", 128, null, 0);
        SetTextureFilter(headerFont.Texture, TextureFilter.Point);
        titleFont = LoadFontEx("Resources/terminal.ttf", GameTitleIdleScreen.FontHeight, null, 0);
        SetTextureFilter(titleFont.Texture, TextureFilter.Point);
		AssetManager.Initiate();
        SetTargetFPS(144);

        _game.Init();
        _game.StartGame();
        while (!WindowShouldClose())
        {
	        if (IsWindowResized())
	        {
		        Resize();
	        }
	        
            // Update loop
            _game.Tick(Raylib.GetFrameTime());
            
            // Draw
            BeginDrawing();
	            ClearBackground(Color.Black);
	            _window.Draw();
	            _game.Draw();
            EndDrawing();
        }

        
        //Unload Resources
		_window.OnClose();
        UnloadFont(terminalFont);
        AssetManager.UnloadAll();
        CloseWindow();

        return 0;
    }

	private static void Resize()
	{
		_window.SetSizes();
	}
}