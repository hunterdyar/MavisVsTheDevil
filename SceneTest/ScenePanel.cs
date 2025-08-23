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
	private int[] animFrame;
	private float _playback = 0;
	private int animCount = 0;
	private Shader hellShader;
	public RenderTexture2D fightScreenTex;
	private int timeLoc;
	private int resolutionLoc;
	public unsafe ScenePanel()
	{
		int width = GetScreenWidth();
		int height = GetScreenHeight();
		
		fightScreenTex = LoadRenderTexture(width, height);
		hellShader = Raylib.LoadShader(null, "Resources/hell.fs");
		//
		timeLoc = GetShaderLocation(hellShader, "iTime");
		resolutionLoc = GetShaderLocation(hellShader, "iResolution");
	}

	public void Reinit()
	{
		UnloadRenderTexture(fightScreenTex);
		fightScreenTex = LoadRenderTexture(Raylib.GetScreenWidth(), GetScreenHeight());
		SetShaderValue(hellShader, resolutionLoc, new Vector2(fightScreenTex.Texture.Width, fightScreenTex.Texture.Height), ShaderUniformDataType.Vec2);

	}

	private bool playing = false;
	public unsafe void Draw()
	{
		// BeginTextureMode(fightScreenTex);
		// ClearBackground(Color.Black);
		// EndTextureMode();

		SetShaderValue(hellShader, timeLoc, (float)Raylib.GetTime(), ShaderUniformDataType.Float);
		BeginShaderMode(hellShader);
			DrawRectangle(0, 0, GetScreenWidth(),GetScreenHeight(),Color.Black);
		EndShaderMode();
		// DrawTextureEx(fightScreenTex.Texture,new Vector2(0,0),0,10,Color.White);

		DrawFPS(0,0);
	}

	public void OnClose()
	{
		UnloadRenderTexture(fightScreenTex);
		UnloadShader(hellShader);
		UnloadModel(plane);
	}

	public void Play()
	{
		playing = true;
		_playback = 0;
	}


}