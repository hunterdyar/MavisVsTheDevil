namespace MavisVsTheDevil.Engine;

public class TitleState : StateBase
{
	//onInput, change state to introduction animation

	public TitleState(StateMachine machine) : base(machine)
	{
	}

	public override void OnEnter()
	{
		Program.GameWindow.SetActiveWindows(Program.GameWindow.TitleIdleScreen);
		base.OnEnter();
	}

	public override void OnExit()
	{
		//UhHH
		// Program.GameWindow.SetActiveWindows(Program.GameWindow.TitleIdleScreen);
		base.OnExit();
	}

	public override void TypeKeyPressed(char key)
	{
		_machine.GoToState(_machine.IntroductionAnimationState);
	}
}