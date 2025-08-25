using MavisVsTheDevil.Animation;
using MavisVsTheDevil.Elements;
using Raylib_cs;

namespace MavisVsTheDevil.Engine;

public class RoundFailed : StateBase
{
	private int _val;
	private readonly TweenBase _tween;
	private readonly Scene _scene;
	
	public RoundFailed(StateMachine machine) : base(machine)
	{
		_tween = new IntTween((x => _val = x), 5, 3);
		_scene = new Scene();
		_scene.SetDemon(true);
	}

	public override void OnEnter()
	{
		_tween.Reset();
		Console.WriteLine("round start ");
		Program.GameWindow.SetActiveWindows(Program.GameWindow.FightWindow, Program.GameWindow.TypingWindow);
		Program.GameWindow.FightWindow.SetScene(_scene);
		AssetManager.Demon?.Play();
		
		base.OnEnter();
	}

	public override void Tick(float delta)
	{
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
			_machine.GoToState(_machine.TitleState);
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
		DrawUtility.DrawLineCentered($"The demon {demon.Name} defeated you.", width, y + 24, 24, Color.White);
		if (_val > 2)
		{
			DrawUtility.DrawLineCentered("You are banished deeper into hell.", width, y + 46, 24, Color.White);
		}
		if (_val > 4)
		{
			DrawUtility.DrawLineCentered($"Why must you struggle?", width, y + 68, 24, Color.White);
		}
	}
}