using MavisVsTheDevil.Animation;
using MavisVsTheDevil.Elements;
using Raylib_cs;

namespace MavisVsTheDevil.Engine;

public class RoundSucceed : StateBase
{
	private int _val;
	private TweenBase _tween;
	private Scene _scene;
	
	public RoundSucceed(StateMachine machine) : base(machine)
	{
		_tween = new IntTween((x => _val = x), 5, 3);
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
		AssetManager.Demon?.Play();
		
		base.OnEnter();
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
			_machine.GoToState(_machine.RoundStartAnimation);
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
		var y = Raylib.GetScreenHeight()/2 + 50;
		DrawUtility.DrawLineCentered($"You have defeated the demon {demon.Name}.", width, y + 24, 24, Color.White);
		DrawUtility.DrawLineCentered($"{demon.Name} is banished deeper into hell.", width, y + 46, 24, Color.White);
		DrawUtility.DrawLineCentered($"The Demon Wails: {demon.Struggle}", width, y + 68, 24, Color.White);
	}
}