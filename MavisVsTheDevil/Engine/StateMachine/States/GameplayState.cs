using Raylib_cs;

namespace MavisVsTheDevil.Engine;

public class GameplayState : StateBase
{
	public GameplayState(StateMachine machine) : base(machine)
	{
		Round.OnRoundStateChanged += OnRoundStateChanged;
	}

	private void OnRoundStateChanged(RoundState state)
	{

	}

	public override void OnEnter()
	{
		Program.GameWindow.SetActiveWindows(Program.GameWindow.FightWindow, Program.GameWindow.TypingWindow);
		base.OnEnter();
	}

	public override void TypeKeyPressed(char key)
	{
		Program.GameWindow.Game.CurrentRound?.Test?.TypeKeyPressed(key);
	}

	public override void Draw()
	{
		if (_machine.Game.CurrentRound != null)
		{
			DrawUtility.DrawLineCentered("Wordlist: " + _machine.Game.CurrentRound.WordlistName, Program.GameWindow.FightWindow.Width, Program.GameWindow.FightWindow.PosY + Program.GameWindow.FightWindow.Height - 30, 28, Color.White);
		}

		base.Draw();
	}
}