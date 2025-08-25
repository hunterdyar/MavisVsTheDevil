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
	public TypingWindow(GameWindow gameWindow) : base(gameWindow)
	{
		_screenTex = LoadRenderTexture(Width, Height);
		_postShader = Raylib.LoadShader(null, "Resources/postBloom.fs");
	}

	protected override void OnResize()
	{
		_centerY = (Height / 2);
		UnloadRenderTexture(_screenTex);
		_screenTex = LoadRenderTexture(Width, Height);
		_wordsPerRow = 20;
	}

	public override void Draw()
	{
		BeginTextureMode(_screenTex);
			ClearBackground(Color.Blank);
			Raylib.DrawRectangle(0, 0, Width, Height, _bg);
			DoDraw();
		EndTextureMode();
		BeginShaderMode(_postShader);
			DrawTextureRec(_screenTex.Texture,
				new Rectangle(0, 0, (float)_screenTex.Texture.Width, (float)-_screenTex.Texture.Height), new Vector2(PosX, PosY), Color.White);
		EndShaderMode();
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
		
		//total lines then offset
		int lines = (int)Math.Ceiling(test.WordCount / (float)_wordsPerRow);
		float vOffset = lines * (FontHeight + LinePadding) / (float)2f;
		int wordY = _centerY - (int) MathF.Floor(vOffset) - FontHeight-LinePadding;
		
		//todo: Get proper width
		int lastWord = -1;
		int paddingX = 20;
		int letterX = paddingX;
		int wordIndex = -1;
		int drawnWords = 0;
		for (int i = 0; i < test.Letters.Count; i++)
		{
			//line break
			
			if (wordIndex != test.Letters[i].Word)
			{
				//word break
				drawnWords++;
				wordIndex =  test.Letters[i].Word;
				// //line break
				if (drawnWords > lastWord)
				{
					int firstWordOnLine = drawnWords;
					lastWord = drawnWords + _wordsPerRow;
					lastWord = lastWord > test.WordCount - 1 ? test.WordCount - 1 : lastWord;
					int lineWidthLetterCount = test.GetLetterCountForFirstNumberWords(firstWordOnLine, lastWord);
					int lineWidth = lineWidthLetterCount * FontWidth;

					if (lineWidth >= Width)
					{
						_wordsPerRow--;
						//the screen will blink black for a frame, which is graceful enough failure.
						return;
					}

					paddingX = (Width - (lineWidth)) / 2;
					
					letterX = paddingX;
					wordY += FontHeight + LinePadding;
				}
			}
			
			DrawLetter(test.Letters[i], ref letterX, wordY);
		}

		

	}

	private void DrawLetter(TestLetter letter, ref int letterX, int wordY)
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

		letterX += FontWidth;
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