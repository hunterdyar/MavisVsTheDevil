using MavisVsTheDevil.Engine;
using Raylib_cs;
using static Raylib_cs.Raylib;
namespace MavisVsTheDevil;

//draws the game!
public class GameWindow
{
	private Game _game;
	private readonly int Width;
	private int fontWidth = 12;
	private int fontHeight = 20;
	private int wordsPerRow = 8;
	public GameWindow(Game game)
	{
		_game = game;
		//todo: parent class for panels/things with positions that can handle being resized. like how i did it for BTN.
		Width = Raylib.GetScreenWidth();
	}

	//on resize
	public void Draw()
	{
		//todo: move to draw test text panel.
		var test = _game.CurrentTest;
		if (test == null)
		{
			return;
		}
		
		int wordY = 300;
		var color = Color.White;
		int totalLineWidth = test.Letters.Count * fontWidth;
		int paddingX = ((Width - totalLineWidth) / 2);//center
		int letterX = paddingX;
		int wordIndex = 0;
		int drawnWords = 0;
		for (int i = 0; i < test.Letters.Count; i++)
		{
			if (wordIndex != test.Letters[i].Word)
			{
				drawnWords++;
				wordIndex =  test.Letters[i].Word;
				
				if (drawnWords >= wordsPerRow)
				{
					drawnWords = 0;
					letterX = paddingX;
					wordY += fontHeight+5;
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
		DrawText(letter.ToString(), letterX, wordY, fontHeight, color);
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
			DrawText(m, letterX, wordY+mistakeOffset, fontHeight, Color.White);
			mistakeOffset += coreMistakeOffset;
		}

		letterX += fontWidth;
	}
}