using System.Numerics;
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
	
	public HellPanel(GameWindow window) : base(window)
	{
		int width = GetScreenWidth();
		int height = GetScreenHeight();

		_hellTex = LoadRenderTexture(width, height);
		_hellShader = Raylib.LoadShader(null, "Resources/hell.fs");
		//
		_timeLoc = GetShaderLocation(_hellShader, "iTime");
		_resolutionLoc = GetShaderLocation(_hellShader, "iResolution");
		_pixelateLoc = GetShaderLocation(_hellShader, "pixelate");
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
		// BeginTextureMode(_hellTex);
		// ClearBackground(Color.Black);
		// EndTextureMode();

		SetShaderValue(_hellShader, _timeLoc, (float)Raylib.GetTime(), ShaderUniformDataType.Float);
		BeginShaderMode(_hellShader);
			DrawRectangle(PosX,PosY, Width, Height, Color.White);
		EndShaderMode();
		DrawFPS(0, 20);
	}
}