using MavisVsTheDevil.GameAnimations;
using MavisVsTheDevil.Panels;

namespace MavisVsTheDevil.Engine;

public class AnimationState : StateBase
{
	private AnimationPanel _animPanel;
	private PanelBase[] AllActivePanels;
	private StateBase _nextState;
	//on skip input or on animationComplete, go to determined state
	public AnimationState(StateMachine machine, AnimationPanel animPanel, StateBase nextState, params PanelBase[] AlsoActivePanels) : base(machine)
	{
		_animPanel = animPanel;
		_nextState = nextState;
		var p = new List<PanelBase>(AlsoActivePanels);
		p.Add(_animPanel);
		this.AllActivePanels =  p.ToArray();

	}
	
	public override void OnEnter()
	{
		_animPanel.Primary.Reset();
		Console.WriteLine("enter animation state");
		//we subscribe and ubsubscribe from the action so that the animations can be reused by multiple states, jic.
		_animPanel.Start();
		// _animPanel.Primary.OnComplete += OnAnimComplete;
		Program.GameWindow.SetActiveWindows(AllActivePanels);
		base.OnEnter();
	}

	private void OnAnimComplete()
	{
		Console.WriteLine("animation complete, exit state!");
		_machine.GoToState(_nextState);
	}
	public override void OnExit()
	{
		Console.WriteLine("exit animation state");
		// _animPanel.Primary.OnComplete -= OnAnimComplete;
		base.OnExit();
	}

	public override void Tick(float delta)
	{
		_animPanel.Tick(delta);
		if (_animPanel.Primary.IsComplete)
		{
			OnAnimComplete();
		}
		base.Tick(delta);
	}
	
}