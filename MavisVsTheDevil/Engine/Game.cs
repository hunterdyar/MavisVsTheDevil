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
		State = new StateMachine(this);
		Round.OnRoundStateChanged += OnRoundStateChanged;
	}

	//race conditions for things that need windows.
	public void Init()
	{
		State.InitMavisStates();
	}

	public void StartGame()
	{
		_round = -1;
		State.GoToState(State.TitleState);
	}
	
	public void StartRound()
	{
		CurrentRound.Start();
	}

	public void StartNewRound()
	{
		_round++;
		CurrentRound = new Round(_round);
		StartRound();
	}

	private void OnRoundStateChanged(RoundState state)
	{
		if (state == RoundState.Complete)
		{
			State.GoToState(State.MoveToNextRoundAnimation);
		}else if (state == RoundState.Failure)
		{
			State.GoToState(State.RoundFailureAnimation);
		}
	}
	public void Tick(float delta)
	{
		var press = Raylib_cs.Raylib.GetCharPressed();
		while (press != 0)
		{
			char c = (char)press;
			if (char.IsAsciiLetterOrDigit(c) || c == ' ' || c == '\'' || c == '-' || c == '=')//= is only for the cheat
			{
				TypeKeyPressed(c);
			}

			press = Raylib_cs.Raylib.GetCharPressed();
		}

		State.Tick(delta);
		
	}

	private void TypeKeyPressed(char key)
	{
		State.TypeKeyPressed(key);
	}


	public void Draw()
	{
		State.Draw();
	}
}