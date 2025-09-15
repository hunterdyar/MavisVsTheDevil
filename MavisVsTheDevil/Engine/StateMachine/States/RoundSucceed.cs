using MavisVsTheDevil.Animation;
using MavisVsTheDevil.Demons;
using MavisVsTheDevil.Elements;
using Raylib_cs;

namespace MavisVsTheDevil.Engine;

public class RoundSucceed : StateBase
{
	private int _val;
	private TweenBase _tween;
	private Scene _scene;
	private int fontSize = 64;
	private readonly TweenBase _timeoutToGoToTitle;

	public RoundSucceed(StateMachine machine) : base(machine)
	{
		_timeoutToGoToTitle = new NopTween(30);

		_tween = new IntTween((x => _val = x), 5, 2.5f);
		_scene = new Scene();
		_scene.SetDemon(true);
		_scene.SetMavis(false);
	}

	public override void OnEnter()
	{
		_tween.Reset();
		Program.GameWindow.SetActiveWindows(Program.GameWindow.FightWindow, Program.GameWindow.TypingWindow);
		Program.GameWindow.TypingWindow.SetTextOpacity(0.5f);
		Program.GameWindow.FightWindow.SetScene(_scene);
		AssetManager.Demon?.Play();
		base.OnEnter();
	}
	public override void Tick(float delta)
	{
		_timeoutToGoToTitle.Tick(delta);

		if (!_tween.IsComplete)
		{
			_tween.Tick(delta);
		}
		else
		{
			//GoToNextState();
		}

		if (_timeoutToGoToTitle.IsComplete)
		{
			//onEnter title resets the game for us.
			_machine.GoToState(_machine.TitleState);
		}

		base.Tick(delta);
	}

	public override void TypeKeyPressed(char key)
	{
		GoToNextState();
		// if (_tween.IsComplete)
		// {
		// 	GoToNextState();
		// }
	}

	private void GoToNextState()
	{
		if (_machine.Game.PastWinState)
		{
			_machine.GoToState(_machine.GameWin);
		}
		else
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
		var y = Program.GameWindow.FightWindow.Height/2 - (fontSize*3)/2;
		DrawUtility.DrawLineCentered($"You have defeated {Demon.TheTitle} {demon.Name}.", width, y, fontSize, Color.White);
		DrawUtility.DrawLineCentered($"{demon.Name} is banished deeper into hell.", width, y + fontSize, fontSize, Color.White);
		DrawUtility.DrawLineCentered($"{Demon.TheTitle} Wails:", width, y + fontSize*2, fontSize, Color.White);
		DrawUtility.DrawLineCentered($"{demon.Struggle}", width, y + fontSize * 3, fontSize-4, Color.White);

	}
}