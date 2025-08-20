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

	override public void OnEnter()
	{
		Program.GameWindow.SetActiveWindows(Program.GameWindow.FightWindow, Program.GameWindow.TypingWindow);
		base.OnEnter();
	}

	override public void OnExit()
	{
		base.OnExit();
	}

	public override void TypeKeyPressed(char key)
	{
		Program.GameWindow.Game.CurrentRound?.Test?.TypeKeyPressed(key);
	}
}