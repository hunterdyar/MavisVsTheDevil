using MavisVsTheDevil.GameAnimations;
using MavisVsTheDevil.Panels;

namespace MavisVsTheDevil.Engine;

public class AnimationState : StateBase
{
	private AnimationPanel _animPanel;

	private StateBase _nextState;
	//on skip input or on animationComplete, go to determined state
	public AnimationState(StateMachine machine, AnimationPanel animPanel, StateBase nextState) : base(machine)
	{
		_animPanel = animPanel;
		_nextState = nextState;
	}
	
	public override void OnEnter()
	{
		//we subscribe and ubsubscribe from the action so that the animations can be reused by multiple states, jic.
		_animPanel.Primary.OnComplete += OnAnimComplete;
		_animPanel.Start();
		Program.GameWindow.SetActiveWindows(_animPanel);
		base.OnEnter();
	}

	private void OnAnimComplete()
	{
		_machine.GoToState(_nextState);
	}
	public override void OnExit()
	{
		_animPanel.Primary.OnComplete -= OnAnimComplete;
		base.OnExit();
	}

	public override void Tick(float delta)
	{
		_animPanel.Tick(delta);
		base.Tick(delta);
	}
	
}