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
	public static int Main()
    {
        // Initialization
        //--------------------------------------------------------------------------------------
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "raylib [text] example - sprite font loading");

        string msg1 = "THIS IS A custom SPRITE FONT...";
        //
        // Vector2 fontPosition1 = new(
        //     screenWidth / 2 - MeasureTextEx(font1, msg1, font1.BaseSize, -3).X / 2,
        //     screenHeight / 2 - font1.BaseSize / 2 - 80
        // );
        //
        SetTargetFPS(60);
        //--------------------------------------------------------------------------------------

        // Main game loop
        while (!WindowShouldClose())
        {
            // Update
            _game.Tick();
            // Draw
            //----------------------------------------------------------------------------------
            BeginDrawing();
            ClearBackground(Color.Black);

            _window.Draw();
           
            EndDrawing();
            //----------------------------------------------------------------------------------
        }

        // De-Initialization
        //--------------------------------------------------------------------------------------
        // UnloadFont(font1);

        CloseWindow();
        //--------------------------------------------------------------------------------------

        return 0;
    }
}