using System.Numerics;
using MavisVsTheDevil.Engine;
using Raylib_cs;
using static Raylib_cs.Raylib;
namespace MavisVsTheDevil.Panels;

public class FightWindow : PanelBase
{
	private Camera3D camera;
	private Model mavis;
	private Vector3 mavisPos;
	private ModelAnimation mavisWalkAnimation;
	private int mavisWalkFrameCounter = 0;

	private Shader postShader;
	public RenderTexture2D fightScreenTex;
	
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
		mavisPos = new Vector3(0, 0, 0);
		var FileInfo = new FileInfo("Resources/models/test/test.glb");
		mavis = LoadModel(FileInfo.FullName);

		int animCount = 0;                                               
		var anims = LoadModelAnimations("Resources/models/test/test.glb",ref animCount);
		if (animCount > 0)
		{
			mavisWalkAnimation = anims[0]; //we ... assume!
		}
		else
		{
			//UHH
		}
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
		
		//Loop walk Animation.
		mavisWalkFrameCounter++;
		UpdateModelAnimation(mavis,mavisWalkAnimation, mavisWalkFrameCounter);
		 if (mavisWalkFrameCounter >= mavisWalkAnimation.FrameCount)
		 {
		 	mavisWalkFrameCounter = 0;
		 }
		
		
		BeginTextureMode(fightScreenTex);
			ClearBackground(Color.Black);
			//DrawRectangle(PosX, PosY, Width, Height, Color.Black);
			DrawCircle(Width/2, Height/2, 400,Color.Red);
			BeginMode3D(camera);
				//DrawGrid(10, 1.0f);
				DrawModelEx(
					mavis,
					mavisPos + new Vector3(0, -2f, 0),
					new Vector3(1.0f, 0.0f, 0.0f),
					0.0f,
					new Vector3(0.1f, .1f, .10f),
					Color.White
				);
			EndMode3D();
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
		UnloadModelAnimation(mavisWalkAnimation);
		UnloadModel(mavis);
		base.OnClose();
	}
}