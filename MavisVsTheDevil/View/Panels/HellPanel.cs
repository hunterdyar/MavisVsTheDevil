using System.Numerics;
using MavisVsTheDevil.Elements;
using Raylib_cs;
using static Raylib_cs.Raylib;
namespace MavisVsTheDevil.Panels;

public class HellPanel : PanelBase
{
	private Shader _hellShader;
	public RenderTexture2D _hellTex;
	private int _timeLoc;
	private int _resolutionLoc;
	private int _pixelateLoc;

	private VisualModel[] holes;
	private Camera3D _camera;
	public HellPanel(GameWindow window) : base(window)
	{
		int width = GetScreenWidth();
		int height = GetScreenHeight();

		_hellTex = LoadRenderTexture(width, height);
		_hellShader = Raylib.LoadShaderFromMemory(null, SHADER.HELL);
		//_hellShader = Raylib.LoadShader(null, "Resources/hell.fs");
		//
		_timeLoc = GetShaderLocation(_hellShader, "iTime");
		_resolutionLoc = GetShaderLocation(_hellShader, "iResolution");
		_pixelateLoc = GetShaderLocation(_hellShader, "pixelate");

		holes = new VisualModel[5];
		for (int i = 0; i < holes.Length; i++)
		{
			holes[i] = new VisualModel("Resources/models/hole.glb", false);
			holes[i].SetRootScale(3);
			holes[i].SetTint(Color.Red);
		}

		_camera = new Camera3D();
		_camera.Position = new Vector3(0, 0, -10);
		_camera.Target = Vector3.Zero;
	}

	protected override void OnResize()
	{
		UnloadRenderTexture(_hellTex);
		_hellTex = LoadRenderTexture(Width, Height);
		SetShaderValue(_hellShader,_resolutionLoc, new Vector2(Width, Height), ShaderUniformDataType.Vec2);
		SetShaderValue(_hellShader, _pixelateLoc, new Vector2(Width/10, Height/10), ShaderUniformDataType.Vec2);

	}
	public override void Draw()
	{
		var delta = Raylib.GetFrameTime();
		Raylib.BeginMode3D(_camera);
		for (int i = 0; i < holes.Length; i++)
		{
			var h = holes[i];
			h.Draw3D(delta);
		}
		EndMode3D();
		// BeginTextureMode(_hellTex);
		// ClearBackground(Color.Black);
		// EndTextureMode();
		if (!Program.UseShaders)
		{
			return;
		}

		// SetShaderValue(_hellShader, _timeLoc, (float)Raylib.GetTime(), ShaderUniformDataType.Float);
		// BeginShaderMode(_hellShader);
		// 	DrawRectangle(PosX,PosY, Width, Height, Color.White);
		// EndShaderMode();
		
		
		DrawFPS(0, 20);
	}
}