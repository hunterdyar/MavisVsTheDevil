using System.Diagnostics;
using MavisVsTheDevil.Panels;

namespace MavisVsTheDevil.Engine;

public class StateMachine
{
	public static Action<StateBase> OnStateEntered;
	public StateBase TitleState;
	public StateBase IntroductionAnimationState;
	public StateBase RoundStartAnimation;
	public StateBase TypeGameplay;
	public StateBase MoveToNextRoundAnimation;
	public StateBase RoundFailureAnimation;
	
	//any states that we "exit to previous" from?
	// paused, lost focus, etc? lets say no
	public string CurrentStateName()
	{
		if (_currentStateBase == TitleState)
		{
			return "Title";
		}else if (_currentStateBase == IntroductionAnimationState)
		{
			return "Introduction Animation";
		}else if (_currentStateBase == RoundStartAnimation)
		{
			return "Round Start Anim";
		}else if (_currentStateBase == TypeGameplay)
		{
			return "Gameplay";
		}else if (_currentStateBase == MoveToNextRoundAnimation)
		{
			return "Round Win Anim";
		}else if (_currentStateBase == RoundFailureAnimation)
		{
			return "Round Fail Anim";
		}

		return "Unknown State";
	}
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
		TypeGameplay = new GameplayState(this);
		TitleState = new TitleState(this);
		RoundStartAnimation = new AnimationState(this, Program.GameWindow.RoundIntroduction, TypeGameplay);
		RoundStartAnimation.OnEnterState += Program.GameWindow.Game.StartNewRound;
		IntroductionAnimationState = new AnimationState(this, Program.GameWindow.TitleIntroAnim, RoundStartAnimation);
		//make new anim for 'walking forwards'
		MoveToNextRoundAnimation = new AnimationState(this, Program.GameWindow.RoundIntroduction, RoundStartAnimation);
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
		switch (key)
		{
			case '1':
				GoToState(TitleState);
				break;
			case '2':
				GoToState(IntroductionAnimationState);
				break;
			case '3':
				GoToState(RoundStartAnimation);
				break;	
			case '4':
				GoToState(RoundFailureAnimation);
				break;
		}
	}
}