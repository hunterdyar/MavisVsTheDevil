using MavisVsTheDevil.Animation;
using MavisVsTheDevil.Elements;
using Raylib_cs;

namespace MavisVsTheDevil.Engine;

public class RoundStartState : StateBase
{
	private TweenBase _tween;
	private int _textVal;
	private Scene _scene;
	public RoundStartState(StateMachine machine) : base(machine)
	{
		_tween = new IntTween((x => _textVal = x), 5, 3);
		_scene = new Scene();
		_scene.SetDemon(true);
	}

	public override void OnEnter()
	{
		Program.GameWindow.Game.StartNewRound();
		_tween.Reset();
		Console.WriteLine("round start ");
		//we subscribe and ubsubscribe from the action so that the animations can be reused by multiple states, jic.
		
		// _animPanel.Primary.OnComplete += OnAnimComplete;
		Program.GameWindow.SetActiveWindows(Program.GameWindow.FightWindow);
		Program.GameWindow.FightWindow.SetScene(_scene);
		AssetManager.Demon.StopAndResetAnim();

		base.OnEnter();
	}
	
	public override void OnExit()
	{
		Console.WriteLine("exit animation state");
		// _animPanel.Primary.OnComplete -= OnAnimComplete;
		base.OnExit();
	}

	public override void Tick(float delta)
	{
		//it's sort of an anti-pattern to stop this here and not internally, but i have some console.writes i want to supress soooooo
		if (!_tween.IsComplete)
		{
			_tween.Tick(delta);
		}

		base.Tick(delta);
	}

	public override void TypeKeyPressed(char key)
	{
		if (_tween.IsComplete)
		{
			_machine.GoToState(_machine.TypeGameplay);
		}	
	}

	public override void Draw()
	{
		var demon = _machine.Game.CurrentRound?.Demon;
		if (demon == null)
		{
			return; 
		}
		int width = Raylib.GetScreenWidth();
		int posY = Raylib.GetScreenHeight()/2;

		DrawUtility.DrawLineCentered($"You Encounter {demon.Name}.", width, posY + 24, 24, Color.White);
		if (_textVal < 1)
		{
			return;
		}

		DrawUtility.DrawLineCentered($"Cause of death: {demon.CauseOfDeath}.", width, posY + 44, 24, Color.White);
		if (_textVal < 2)
		{
			return;
		}

		DrawUtility.DrawLineCentered("It gives you modifiers: ...", width, posY + 88, 24, Color.White);

		DrawUtility.DrawLineCentered("... ready? ... ", width, posY + 108, 12, Color.White);

	}
}