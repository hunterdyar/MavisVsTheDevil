using System.CodeDom.Compiler;

namespace MavisVsTheDevil.Engine;

public class Game
{
	public TypeTest? CurrentTest => CurrentRound.Test;

	public Round CurrentRound;
	private int _round = 0;
	public Game()
	{
		Round.OnRoundStateChanged += OnRoundStateChanged;

		StartGame();
	}

	private void StartGame()
	{
		_round = 0;
		CurrentRound = new Round(_round);
		StartRound();
	}


	public void StartRound()
	{
		//first we are animating. then when the tween is done, we start the round.
		CurrentRound.Start();
		//when the round ends, we start the next round transition animation (see onroundstatechange)
	}

	private void OnRoundStateChanged(RoundState state)
	{
		if (state == RoundState.Complete)
		{
			// end state and start animation for next state.
			_round++;
			CurrentRound = new Round(_round);
			StartRound();
		}
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