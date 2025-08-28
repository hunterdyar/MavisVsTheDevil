using System.Numerics;
using MavisVsTheDevil.Engine;
using Raylib_cs;
using static Raylib_cs.Raylib;
namespace MavisVsTheDevil.Panels;

public class TypingWindow :PanelBase
{
	private Game Game => _window.Game;
	private const int FontWidth = 20;
	private const int FontHeight = 32;
	private int _wordsPerRow = 20;
	private int _centerY;
	private const int LinePadding = 5;
	private Color _bg = new Color(0, 0, 0, 0.75f);
	private readonly Shader _postShader;
	private RenderTexture2D _screenTex;
	private Color _textColor = Color.White;
	private Color _passedTextColor = Color.Gold;
	private TextLayout _textLayout;
	public TypingWindow(GameWindow gameWindow) : base(gameWindow)
	{
		_screenTex = LoadRenderTexture(Width, Height);
		//_postShader = Raylib.LoadShader(null, "Resources/postBloom.fs");
		_postShader = LoadShaderFromMemory(null, SHADER.BLOOM);
		_textLayout = new TextLayout(50);
	}

	protected override void OnResize()
	{
		_centerY = (Height / 2);
		UnloadRenderTexture(_screenTex);
		_screenTex = LoadRenderTexture(Width, Height);
		_wordsPerRow = 20;
		int maxWidth = (int)MathF.Floor((Width-1) / (float)FontWidth)-2;
		_textLayout.SetMaxWidth(maxWidth);

	}

	public override void Draw()
	{
		BeginTextureMode(_screenTex);
			ClearBackground(Color.Blank);
			Raylib.DrawRectangle(0, 0, Width, Height, _bg);
			DoDraw();
		EndTextureMode();
		if (Program.UseShaders)
		{
			BeginShaderMode(_postShader);
			DrawTextureRec(_screenTex.Texture,
				new Rectangle(0, 0, (float)_screenTex.Texture.Width, (float)-_screenTex.Texture.Height),
				new Vector2(PosX, PosY), Color.White);
			EndShaderMode();
		}
		else
		{
			DrawTextureRec(_screenTex.Texture,
				new Rectangle(0, 0, (float)_screenTex.Texture.Width, (float)-_screenTex.Texture.Height),
				new Vector2(PosX, PosY), Color.White);
		}
	}
	private void DoDraw(){

		var test = Game.CurrentTest;
		if (test == null)
		{
			return;
		}
		
		var percentage = 1-Math.Clamp(test.Elapsed / test._allowedTime, 0, 1);
		int width = (int)(percentage * Width);
		Raylib.DrawRectangle(0, 0, width, 25, Color.Red);

		int xPadding = 0;
		int linePaddingCalculatedFor = -1;
		int vPadding = (int)((Height - (_textLayout.TotalLines * FontHeight) + 1) / 2);
		if (vPadding < 0)
		{
			vPadding = 0;
		}

		for (int i = 0; i < test.Letters.Count; i++)
		{
			var lf = _textLayout.Get(i);
			if (lf.Line != linePaddingCalculatedFor)
			{
				linePaddingCalculatedFor = lf.Line;
				xPadding = (int)((Width - (_textLayout.LineWidths[lf.Line]*FontWidth)) / (float)2);
				Console.WriteLine(lf.Line+": "+xPadding);
			}
			//todo: we don't have to keep calculating this.
			DrawLetter(test.Letters[i],lf.Column*FontWidth + xPadding, lf.Line*FontHeight + vPadding);
		}
	}

	private void DrawLetter(TestLetter letter, int letterX, int wordY)
	{
		var color = _textColor;
		
		//if gamestate is active:
		if (letter.State == LetterState.Failure)
		{
			color = Color.Red;
		}else if (letter.State == LetterState.Pass)
		{
			color = _passedTextColor;
		}else if (letter.State == LetterState.Current)
		{
			color = Color.Black;
		}

		if (letter.State == LetterState.Current || letter.State == LetterState.Failure)
		{
			DrawRectangle(letterX-1, wordY, FontWidth, FontHeight, Color.White);
		}
		
		DrawTextEx(Program.terminalFont,letter.ToString(), new Vector2(letterX, wordY), FontHeight,0, color);
		int coreMistakeOffset = -(int)(FontHeight * .8f);
		int mistakeOffset = coreMistakeOffset;//just a little scrunch
		foreach (char mistake in letter.Mistakes)
		{
			string m = mistake.ToString();
			if (mistake == ' ')
			{
				m = "_";
			}

			DrawRectangle(letterX - 1, wordY + mistakeOffset, FontWidth, FontHeight, Color.Red);
			DrawTextEx(Program.terminalFont,m, new Vector2(letterX, wordY+mistakeOffset), FontHeight,0, Color.White);
			mistakeOffset += coreMistakeOffset;
		}
	}

	public override void OnClose()
	{
		UnloadRenderTexture(_screenTex);
		UnloadShader(_postShader);
	}


	public void SetTextOpacity(float f)
	{
		_textColor = new Color(_textColor.R,
			_textColor.G, _textColor.B, f);
		_passedTextColor = new Color(_passedTextColor.R, _passedTextColor.G, _passedTextColor.B, f);
	}
}