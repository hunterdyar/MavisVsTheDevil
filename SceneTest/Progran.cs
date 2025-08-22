using Raylib_cs;
using static Raylib_cs.Raylib;
namespace SceneTest;

public static class Program
{
	public static int Main()
	{
		const int screenWidth = 1920;
		const int screenHeight = 1080;
		SetConfigFlags(ConfigFlags.ResizableWindow);
		InitWindow(screenWidth, screenHeight, "Mavis Vs. The Devil");
		//Load Resources
		var terminalFont = LoadFont("Resources/terminal F4.ttf");

		SetTargetFPS(144);

		var scene = new ScenePanel();

		scene.Reinit();
		while (!WindowShouldClose())
		{
			if (IsWindowResized())
			{
				scene.Reinit();
			}

			// Update loop

			// Draw
			BeginDrawing();
			ClearBackground(Color.RayWhite);
			scene.Draw();
			EndDrawing();
			
			//
			var c = Raylib.GetCharPressed();
			while (c != 0)
			{
				if ((char)c == ' ')
				{
					scene.Play();
				}

				c = GetCharPressed();
			}
		}


		//Unload Resources
		scene.OnClose();
		UnloadFont(terminalFont);

		CloseWindow();

		return 0;
	}
}