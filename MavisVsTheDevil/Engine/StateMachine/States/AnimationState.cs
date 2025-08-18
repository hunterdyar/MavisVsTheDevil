namespace MavisVsTheDevil.Engine;

public class AnimationState : StateBase
{
	//on skip input or on animationComplete, go to determined state
	public AnimationState(StateMachine machine) : base(machine)
	{
	}
	
	public override void OnEnter()
	{
		base.OnEnter();
	}

	public override void OnExit()
	{
		base.OnExit();
	}
}