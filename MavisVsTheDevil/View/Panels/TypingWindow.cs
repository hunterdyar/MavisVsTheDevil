using System.Numerics;
using MavisVsTheDevil.Engine;
using Raylib_cs;
using static Raylib_cs.Raylib;
namespace MavisVsTheDevil.Panels;

public class TypingWindow :PanelBase
{
	private GameWindow _gameWindow;
	private Game _game => _gameWindow.Game;
	private int fontWidth = 20;
	private int fontHeight = 32;
	private int wordsPerRow = 10;
	private int centerY;
	private int linePadding = 5;
	
	private Shader postShader;
	public RenderTexture2D screenTex;

	public TypingWindow(GameWindow gameWindow)
	{
		_gameWindow = gameWindow;
		screenTex = LoadRenderTexture(Width, Height);
		postShader = Raylib.LoadShader(null, "Resources/post.glsl");
	}

	protected override void OnResize()
	{
		centerY = (Height / 2);
		UnloadRenderTexture(screenTex);
		screenTex = LoadRenderTexture(Width, Height);
	}

	public override void Draw()
	{
		BeginTextureMode(screenTex);
			ClearBackground(Color.Black);
			DoDraw();
		EndTextureMode();
		BeginShaderMode(postShader);
			DrawTextureRec(screenTex.Texture,
				new Rectangle(0, 0, (float)screenTex.Texture.Width, (float)-screenTex.Texture.Height), new Vector2(PosX, PosY), Color.White);
		EndShaderMode();
	}
	private void DoDraw(){
		//todo: move to draw test text panel.
		var test = _game.CurrentTest;
		if (test == null)
		{
			return;
		}
		
		//total lines then offset
		int lines = (int)Math.Ceiling(test.WordCount / (float)wordsPerRow);
		float vOffset = lines * (fontHeight + linePadding) / (float)2f;
		int wordY = centerY - (int) MathF.Floor(vOffset) - fontHeight-linePadding;
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
					lastWord = drawnWords + wordsPerRow;
					lastWord = lastWord > test.WordCount - 1 ? test.WordCount - 1 : lastWord;
					int lineWidthLetterCount = test.GetLetterCountForFirstNumberWords(firstWordOnLine, lastWord);
					int lineWidth = lineWidthLetterCount * fontWidth;
					if (lineWidth > Width)
					{
						wordsPerRow--;
						//the screen will blink black for a frame, which is graceful enough failure.
						return;
					}
					paddingX = (Width - (lineWidth))/2;
					
					letterX = paddingX;
					wordY += fontHeight + linePadding;
				}
			}
			
			DrawLetter(test.Letters[i], ref letterX, wordY);
		}
		
	}

	private void DrawLetter(TestLetter letter, ref int letterX, int wordY)
	{
		var color = Color.White;
		
		//if gamestate is active:
		if (letter.State == LetterState.Failure)
		{
			color = Color.Red;
		}else if (letter.State == LetterState.Pass)
		{
			color = Color.Gold;
		}else if (letter.State == LetterState.Current)
		{
			color = Color.Black;
		}

		if (letter.State == LetterState.Current || letter.State == LetterState.Failure)
		{
			DrawRectangle(letterX-1, wordY, fontWidth, fontHeight, Color.White);
		}
		DrawTextEx(Program.terminalFont,letter.ToString(), new Vector2(letterX, wordY), fontHeight,0, color);
		int coreMistakeOffset = -(int)(fontHeight * .8f);
		int mistakeOffset = coreMistakeOffset;//just a little scrunch
		foreach (char mistake in letter.Mistakes)
		{
			string m = mistake.ToString();
			if (mistake == ' ')
			{
				m = "_";
			}

			DrawRectangle(letterX - 1, wordY + mistakeOffset, fontWidth, fontHeight, Color.Red);
			DrawTextEx(Program.terminalFont,m, new Vector2(letterX, wordY+mistakeOffset), fontHeight,0, Color.White);
			mistakeOffset += coreMistakeOffset;
		}

		letterX += fontWidth;
	}

	public override void OnClose()
	{
		UnloadRenderTexture(screenTex);
		UnloadShader(postShader);
	}
}