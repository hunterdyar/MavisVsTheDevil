using System.Diagnostics;

namespace MavisVsTheDevil.Engine;

public class StateMachine
{
	public static Action<StateBase> OnStateEntered;
	public StateBase TitleState;
	public StateBase IntroductionAnimationState;
	
	//any states that we "exit to previous" from?
	// paused, lost focus, etc? lets say no
	private StateBase _currentStateBase;

	public StateMachine()
	{
		
	}

	public void Tick(float delta)
	{
		_currentStateBase?.Tick(delta);
	}

	//this is a generic solution, but ...not
	public void InitMavisStates()
	{
		TitleState = new TitleState(this);
		IntroductionAnimationState = new AnimationState(this, Program.GameWindow.TitleIntroAnim, TitleState);
	}
	
	public void GoToState(StateBase stateBase)
	{
		if (stateBase == _currentStateBase)
		{
			throw new Exception("Should not enter same state as was already in!");
		}

		if (stateBase == null)
		{
			throw new Exception("Cannot enter null state");
		}

		if (_currentStateBase != null)
		{
			_currentStateBase.OnExit();
		}
		
		_currentStateBase = stateBase;
		
		_currentStateBase.OnEnter();
		OnStateEntered?.Invoke(_currentStateBase);
	}

	public void TypeKeyPressed(char key)
	{
		_currentStateBase?.TypeKeyPressed(key);
	}
}