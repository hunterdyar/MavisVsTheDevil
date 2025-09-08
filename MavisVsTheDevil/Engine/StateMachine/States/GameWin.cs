using MavisVsTheDevil.Animation;
using MavisVsTheDevil.Demons;
using MavisVsTheDevil.Elements;
using Raylib_cs;

namespace MavisVsTheDevil.Engine;

public class GameWin : StateBase
{
	private int _val;
	private readonly TweenBase _tween;
	private readonly Scene _scene;
	
	public GameWin(StateMachine machine) : base(machine)
	{
		_tween = new IntTween((x => _val = x), 5, 3);
		_scene = new Scene();
		_scene.SetDemon(true);
	}

	public override void OnEnter()
	{
		_tween.Reset();
		Console.WriteLine("game win ");
		Program.GameWindow.SetActiveWindows(Program.GameWindow.TypingWindow);
		//Program.GameWindow.FightWindow.SetScene(_scene);
		//AssetManager.Demon?.Play();
		Program.GoUpALayerOfHell();

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

		int fontSize = 24;
		int width = Raylib.GetScreenWidth();
		var y = Program.GameWindow.FightWindow.Height/2 - (fontSize*3)/2;
		DrawUtility.DrawLineCentered($"You Defeat {Demon.TheTitle}.", width, y, fontSize, Color.White);
		if (_val > 2)
		{
			DrawUtility.DrawLineCentered("You claw yourself one layer up.", width, y + fontSize, fontSize, Color.White);
		}
		if (_val > 3)
		{
			DrawUtility.DrawLineCentered("Congratulations. You are not yet safe.", width, y + fontSize*2, fontSize, Color.White);
		}
		
		if (_val > 4)
		{
			DrawUtility.DrawLineCentered($"Why do you want struggle to be remembered?", width, y + fontSize*2, fontSize, Color.White);
		}
	}
}