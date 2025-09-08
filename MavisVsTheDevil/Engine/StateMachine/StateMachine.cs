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
	public StateBase GameWin;

	public Game Game => _game;
	private Game _game;
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
		}else if (_currentStateBase == GameWin)
		{
			return "Game Win";
		}

		return "Unknown State";
	}
	private StateBase _currentStateBase;

	public StateMachine(Game game)
	{
		_game = game;
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
		RoundStartAnimation = new RoundStartState(this);
		IntroductionAnimationState = new IntroductionState(this);
		//make new anim for 'walking forwards'
		MoveToNextRoundAnimation = new RoundSucceed(this);
		RoundFailureAnimation = new RoundFailed(this);
		GameWin = new GameWin(this);
	}
	
	public void GoToState(StateBase stateBase)
	{
		if (stateBase == _currentStateBase)
		{
			throw new Exception($"Should not enter same state as was already in! {CurrentStateName()}");
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

	public void Draw()
	{
		_currentStateBase.Draw();
	}
}