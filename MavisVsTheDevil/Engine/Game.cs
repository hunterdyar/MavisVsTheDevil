using System.CodeDom.Compiler;

namespace MavisVsTheDevil.Engine;

public class Game
{
	public TypeTest? CurrentTest => CurrentRound?.Test;
	public StateMachine State;
	public Round CurrentRound;
	private int _round = 0;
	public Game()
	{
		State = new StateMachine();
		State.InitMavisStates();
		Round.OnRoundStateChanged += OnRoundStateChanged;
	}

	public void StartGame()
	{
		State.GoToState(State.TitleState);
		//move to ... one of the states?
		// _round = 0;
		// CurrentRound = new Round(_round);
		// StartRound();
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

		State.Tick();
	}

	private void TypeKeyPressed(char key)
	{
		State.TypeKeyPressed(key);
		CurrentRound?.Test?.TypeKeyPressed(key);
	}
}