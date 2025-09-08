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
	
	public RoundSucceed(StateMachine machine) : base(machine)
	{
		_tween = new IntTween((x => _val = x), 5, 2.5f);
		_scene = new Scene();
		_scene.SetDemon(true);
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
		if (!_tween.IsComplete)
		{
			_tween.Tick(delta);
		}
		else
		{
			GoToNextState();
		}

		base.Tick(delta);
	}

	public override void TypeKeyPressed(char key)
	{
		GoToNextState();
		if (_tween.IsComplete)
		{
			GoToNextState();
		}
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
		var y = Raylib.GetScreenHeight()/2 + 50;
		DrawUtility.DrawLineCentered($"You have defeated the {Demon.Title} {demon.Name}.", width, y + 24, 24, Color.White);
		DrawUtility.DrawLineCentered($"{demon.Name} is banished deeper into hell.", width, y + 46, 24, Color.White);
		DrawUtility.DrawLineCentered($"{Demon.TitleStandalone} Wails: {demon.Struggle}", width, y + 68, 24, Color.White);
	}
}