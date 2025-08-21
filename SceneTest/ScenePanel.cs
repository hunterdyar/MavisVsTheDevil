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
	private Model plane;
	private Vector3 mavisPos;
	private ModelAnimation[] animations;
	private int animFrame = 0;
	private int animCount = 0;
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
		var fileInfo = new FileInfo("Resources/models/scenetest/planepop.glb");

		if (fileInfo.Exists)
		{
			plane = LoadModel(fileInfo.FullName);
		}
		else
		{
			throw new Exception("Model not found");
		}

		//BoundingBox bounds = GetMeshBoundingBox(mavis.Meshes[0]);

		var anims = LoadModelAnimations("Resources/models/scenetest/planepop.glb",ref animCount);
		animations = new ModelAnimation[animCount];
		for (int i = 0; i < animCount; i++)
		{
			animations[i] = anims[i]; //we ... assume!
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
		// animFrame++;
		// if (animFrame >= animations[0].FrameCount)
		// {
		// 	animFrame = 0;
		// }
		// for (int i = 0; i < animations.Length; i++)
		// {
		// 	UpdateModelAnimation(plane, animations[i], 0);
		// }

		UpdateModelAnimation(plane, animations[0], 0);



		BeginTextureMode(fightScreenTex);
		
			ClearBackground(Color.Black);
			DrawCircle(GetScreenWidth()/2, GetScreenHeight()/2, 400,Color.Red);
			BeginMode3D(camera);
				//DrawGrid(10, 1.0f);
				
				DrawModelEx(
					plane,
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
		DrawFPS(0,0);
	}

	public void OnClose()
	{
		UnloadRenderTexture(fightScreenTex);
		UnloadShader(postShader);
		foreach (var animation in animations)
		{
			UnloadModelAnimation(animation);

		}
		UnloadModel(plane);
	}
}