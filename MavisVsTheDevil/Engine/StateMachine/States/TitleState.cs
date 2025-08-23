namespace MavisVsTheDevil.Engine;

public class TitleState : StateBase
{
	public TitleState(StateMachine machine) : base(machine)
	{
	}

	public override void OnEnter()
	{
		Program.GameWindow.SetActiveWindows(Program.GameWindow.TitleIdleScreen);
		base.OnEnter();
	}

	public override void TypeKeyPressed(char key)
	{
		_machine.GoToState(_machine.IntroductionAnimationState);
	}
}