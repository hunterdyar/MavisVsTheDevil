using MavisVsTheDevil;
using MavisVsTheDevil.Panels;

namespace SceneTest;

using System.Numerics;
using MavisVsTheDevil.Engine;
using Raylib_cs;
using static Raylib_cs.Raylib;

public class ScenePanel
{
	private Camera3D camera;
	private Model mavis;
	private Vector3 mavisPos;
	private ModelAnimation mavisWalkAnimation;
	private int mavisWalkFrameCounter = 0;

	private Shader postShader;
	public RenderTexture2D fightScreenTex;
	
	public unsafe ScenePanel()
	{
		int width = GetScreenWidth();
		int height = GetScreenHeight();
		
		
		fightScreenTex = LoadRenderTexture(width, height);
		postShader = Raylib.LoadShader(null, "Resources/postPixel.fs");
		//
		camera = new Camera3D();
		camera.Position = new Vector3(0, 5, -10f);
		camera.Target = new Vector3(0, 0, 0);
		camera.Up = new Vector3(0, 1, 0);
		camera.FovY = 80;
		camera.Projection = CameraProjection.Perspective;
		mavisPos = new Vector3(0, 0, 0);
		var fileInfo = new FileInfo("Resources/models/sceneTest.glb");
		 fileInfo = new FileInfo("Resources/models/scenetest/sceneTest.glb");

		if (fileInfo.Exists)
		{
			mavis = LoadModel(fileInfo.FullName);
		}
		else
		{
			throw new Exception("Model not found");
		}

		BoundingBox bounds = GetMeshBoundingBox(mavis.Meshes[0]);


		int animCount = 0;                                               
		var anims = LoadModelAnimations("Resources/models/sceneTest.glb",ref animCount);
		if (animCount > 0)
		{
			mavisWalkAnimation = anims[0]; //we ... assume!
		}
		else
		{
			//UHH
		}
	}

	public void Reinit()
	{
		UnloadRenderTexture(fightScreenTex);
		fightScreenTex = LoadRenderTexture(Raylib.GetScreenWidth(), GetScreenHeight());
	}

	public unsafe void Draw()
	{
		//todo: tick
		
		//rotate camera slowly
		camera.Position = new  Vector3(MathF.Sin((float)Raylib.GetTime()/8f)*10, 5, MathF.Sin(Single.Pi/2+(float)Raylib.GetTime() / 8f) * 10);
		//Loop walk Animation.
		mavisWalkFrameCounter++;
		//UpdateModelAnimation(mavis,mavisWalkAnimation, mavisWalkFrameCounter);
		 if (mavisWalkFrameCounter >= mavisWalkAnimation.FrameCount)
		 {
		 	mavisWalkFrameCounter = 0;
		 }
		
		
		BeginTextureMode(fightScreenTex);
		
			ClearBackground(Color.Black);
			//DrawRectangle(PosX, PosY, Width, Height, Color.Black);
			DrawCircle(GetScreenWidth()/2, GetScreenHeight()/2, 400,Color.Red);
			BeginMode3D(camera);
				//DrawGrid(10, 1.0f);
				
				DrawModelEx(
					mavis,
					new Vector3(0, 0, 0),
					new Vector3(0.0f, 1.0f, 0.0f), 
					0.0f,
					new Vector3(1f, 1f, 1f),
					Color.White
				);
				
			EndMode3D();
		 EndTextureMode();
		
		BeginShaderMode(postShader);
			DrawTextureRec(fightScreenTex.Texture,
			new Rectangle(0, 0, (float)fightScreenTex.Texture.Width, (float)-fightScreenTex.Texture.Height), new Vector2(0, 0), Color.White);
			EndShaderMode();
		
	}

	public void OnClose()
	{
		UnloadRenderTexture(fightScreenTex);
		UnloadShader(postShader);
		UnloadModelAnimation(mavisWalkAnimation);
		UnloadModel(mavis);
	}
}