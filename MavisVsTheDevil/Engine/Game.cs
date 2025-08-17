using System.CodeDom.Compiler;

namespace MavisVsTheDevil.Engine;

public class Game
{
	public TypeTest? CurrentTest => CurrentRound.Test;

	private Round CurrentRound;
	public Game()
	{
		CurrentRound = new Round(0);
	}

	public void StartRound()
	{
		//increase number, reset game state.
	}
	public void Tick()
	{
		var press = Raylib_cs.Raylib.GetCharPressed();
		while (press != 0)
		{
			char c = (char)press;
			if (char.IsAsciiLetterOrDigit(c) || c == ' ' || c == '\'' || c == '-')
			{
				TypeKeyPressed(c);
			}

			press = Raylib_cs.Raylib.GetCharPressed();
		}
		
	}

	private void TypeKeyPressed(char key)
	{
		//check state.
		CurrentRound?.Test?.TypeKeyPressed(key);
	}
}