using System.Numerics;
using MavisVsTheDevil.Elements;
using MavisVsTheDevil.Engine;
using Raylib_cs;
using static Raylib_cs.Raylib;
namespace MavisVsTheDevil.Panels;

public class FightWindow : PanelBase
{
	public static Camera3D Camera;
	private readonly Shader _postShader;
	private RenderTexture2D _fightScreenTex;
	public Scene? ActiveScene => _activeScene;
	private Scene? _activeScene;
	
	public unsafe FightWindow(GameWindow gameWindow) : base(gameWindow)
	{
		_fightScreenTex = LoadRenderTexture(Width, Height);
		_postShader = Raylib.LoadShader(null, "Resources/postPixel.fs");
		
		//make a camera. we'll share it across the scenes with it being static, which is - yes - dumb.
		Camera = new Camera3D();
		Camera.Position = new Vector3(0, 5, -10f);
		Camera.Target = new Vector3(0, 0, 0);
		Camera.Up = new Vector3(0, 1, 0);
		Camera.FovY = 75;
		Camera.Projection = CameraProjection.Perspective;
	}

	protected override void OnResize()
	{
		UnloadRenderTexture(_fightScreenTex);
		_fightScreenTex = LoadRenderTexture(Width, Height);
	}

	public override void Draw()
	{
		Camera.Position = new  Vector3(MathF.Sin((float)Raylib.GetTime()/8f)*10, 5, MathF.Sin(Single.Pi/2+(float)Raylib.GetTime() / 8f) * 10);
		
		BeginTextureMode(_fightScreenTex);
			ClearBackground(Color.Black);
			DrawCircle(Width/2, Height/2, 400,Color.Red);
			_activeScene?.Draw();
		EndTextureMode();
		
		BeginShaderMode(_postShader);
			DrawTextureRec(_fightScreenTex.Texture,
			new Rectangle(0, 0, (float)_fightScreenTex.Texture.Width, (float)-_fightScreenTex.Texture.Height), new Vector2(PosX, PosY), Color.White);
		EndShaderMode();
	}

	public override void OnClose()
	{
		UnloadRenderTexture(_fightScreenTex);
		UnloadShader(_postShader);
		base.OnClose();
	}

	public void SetScene(Scene scene)
	{
		if (scene != null)
		{
			_activeScene = scene;
		}
	}
}
