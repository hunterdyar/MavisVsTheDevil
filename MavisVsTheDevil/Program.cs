using System.Numerics;
using MavisVsTheDevil;
using MavisVsTheDevil.Engine;
using Raylib_cs;
using static Raylib_cs.Raylib;

public static class Program
{
    public static Random random = new Random();
    private static Game _game = new Game();
    private static GameWindow _window = new GameWindow(_game);
    public const int GLSL_VERSION = 330;
    public static Font terminalFont;
	public static int Main()
    {
        const int screenWidth = 800;
        const int screenHeight = 450;
        InitWindow(screenWidth, screenHeight, "Mavis Vs. The Devil");//youre shitting me, i had to do this first.
    
        //example says         // NOTE: Defining null (NULL) for vertex shader forces usage of internal default vertex shader
        //AND YET.....
        Shader postShader = Raylib.LoadShader(null, "Resources/post.glsl");
        terminalFont = LoadFont("Resources/terminal F4.ttf");
         RenderTexture2D screenTex = LoadRenderTexture(screenWidth, screenHeight);

        SetTargetFPS(144);

        while (!WindowShouldClose())
        {
            // Update
            _game.Tick();
            // Draw
            
            BeginDrawing();
            BeginTextureMode(screenTex);
            ClearBackground(Color.Black);

            _window.Draw();
            EndTextureMode();
            
           BeginShaderMode(postShader);
           DrawTextureRec(screenTex.Texture, new Rectangle( 0, 0, (float)screenTex.Texture.Width, (float)-screenTex.Texture.Height ), new Vector2(0,0), Color.White);
           EndShaderMode();
            EndDrawing();
        }

        UnloadRenderTexture(screenTex);
        UnloadShader(postShader);
        UnloadFont(terminalFont);
        CloseWindow();

        return 0;
    }
    
}