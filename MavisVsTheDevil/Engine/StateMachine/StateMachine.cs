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

	public void Tick()
	{
		_currentStateBase?.Tick();
	}

	//this is a generic solution, but ...not
	public void InitMavisStates()
	{
		TitleState = new TitleState(this);
		IntroductionAnimationState = new AnimationState(this);
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