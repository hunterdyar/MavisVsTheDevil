using System.Numerics;
using MavisVsTheDevil.Elements;
using MavisVsTheDevil.Engine;
using Raylib_cs;
using static Raylib_cs.Raylib;
namespace MavisVsTheDevil.Panels;

public class FightWindow : PanelBase
{
	private Camera3D camera;
	private Shader postShader;
	public RenderTexture2D fightScreenTex;
	//
	
	private Scene _meetDemonScene;
	public Scene? ActiveScene => _activeScene;
	private Scene? _activeScene;
	
	public unsafe FightWindow(GameWindow gameWindow) : base(gameWindow)
	{
		fightScreenTex = LoadRenderTexture(Width, Height);
		postShader = Raylib.LoadShader(null, "Resources/postPixel.fs");
		//
		camera = new Camera3D();
		camera.Position = new Vector3(0, 5, -10f);
		camera.Target = new Vector3(0, 0, 0);
		camera.Up = new Vector3(0, 1, 0);
		camera.FovY = 75;
		camera.Projection = CameraProjection.Perspective;
		
		//
		
		_meetDemonScene = new Scene(camera);
		_meetDemonScene.CreateDemonScene();
	}

	protected override void OnResize()
	{
		UnloadRenderTexture(fightScreenTex);
		fightScreenTex = LoadRenderTexture(Width, Height);
	}

	public override void Draw()
	{
		//todo: tick
		
		//rotate camera slowly
		camera.Position = new  Vector3(MathF.Sin((float)Raylib.GetTime()/8f)*10, 5, MathF.Sin(Single.Pi/2+(float)Raylib.GetTime() / 8f) * 10);
		
		BeginTextureMode(fightScreenTex);
			ClearBackground(Color.Black);
			//DrawRectangle(PosX, PosY, Width, Height, Color.Black);
			DrawCircle(Width/2, Height/2, 400,Color.Red);
			_activeScene?.Draw();
		EndTextureMode();
		
		BeginShaderMode(postShader);
			DrawTextureRec(fightScreenTex.Texture,
			new Rectangle(0, 0, (float)fightScreenTex.Texture.Width, (float)-fightScreenTex.Texture.Height), new Vector2(PosX, PosY), Color.White);
		EndShaderMode();
	}

	public override void OnClose()
	{
		UnloadRenderTexture(fightScreenTex);
		UnloadShader(postShader);
		_meetDemonScene.Dispose();
		base.OnClose();
	}

	public void SetScene(FightScene scene)
	{
		switch (scene)
		{
			case FightScene.MeetDemon:
				_activeScene = _meetDemonScene;
				break;
			default:
				_activeScene = _meetDemonScene; 
				break;
		}
		//
	}
}

public enum FightScene{
	MeetDemon,
	IdleDemon,
	DemonExplode,
	PlayerExplode,
}