using System.CodeDom.Compiler;
using Raylib_cs;

namespace MavisVsTheDevil.Engine;

public class Game
{
	public TypeTest? CurrentTest => CurrentRound?.Test;
	public StateMachine State;
	public Round CurrentRound;
	public static bool IsCapsDown;
	public static bool IsShiftDown;

	public bool PastWinState => _round >= 5;// >=2 will trigger after round 3. 
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

			// if (_round == 6)
			// {
			// 	//todo
			// 	State.GoToState(State.GameWin);
			// 	//victory is the players! increase the layer of hell and let them know they won.
			// 	Console.WriteLine("victory. should reset here but doesn't yet.");
			// 	Program.GoUpALayerOfHell();
			// }
		}else if (state == RoundState.Failure)
		{
			State.GoToState(State.RoundFailureAnimation);
		}
	}
	public void Tick(float delta)
	{
		IsCapsDown = Raylib.IsKeyDown(KeyboardKey.CapsLock);
		IsShiftDown = Raylib.IsKeyDown(KeyboardKey.LeftShift) || Raylib.IsKeyDown(KeyboardKey.RightShift);
		var press = Raylib_cs.Raylib.GetCharPressed();
		while (press != 0)
		{
			char c = (char)press;
			//added the programming languages and need more chars lol
			if (char.IsAsciiLetterOrDigit(c) || c == ' ' || c == '\'' || c == '-' || c == '=' || c == '+' || c == '=' || c == ':' || c == '@' || c == '$' || c == '%' || c == '*')//= is only for the cheat
			{
				//this ignores capslock by reversing it, but still respecting the shift key.
				if ((IsCapsDown && !IsShiftDown))
				{
					TypeKeyPressed(char.ToLower(c));	
				}else if (IsCapsDown && IsShiftDown)
				{
					TypeKeyPressed(char.ToUpper(c));
				}
				else
				{
					TypeKeyPressed(c);
				}
			}

			press = Raylib_cs.Raylib.GetCharPressed();
		}

		var key = Raylib_cs.Raylib.GetKeyPressed();
		while (key != 0)
		{
			var kbk = (KeyboardKey)key;
			if (kbk == KeyboardKey.Backspace)
			{
				Console.WriteLine("Backspace!");
			}
			key = Raylib_cs.Raylib.GetKeyPressed();
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

	public void ResetGame()
	{
		_round = -1;
		//reset "failed words" tracking
	}
}