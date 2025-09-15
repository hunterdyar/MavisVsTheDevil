using MavisVsTheDevil.Animation;
using MavisVsTheDevil.Demons;
using MavisVsTheDevil.Elements;
using Raylib_cs;

namespace MavisVsTheDevil.Engine;

public class RoundFailed : StateBase
{
	private int _val;
	private readonly TweenBase _tween;
	private readonly TweenBase _timeoutToGoToTitle;
	private readonly Scene _scene;
	private readonly TweenBase _fadeScreenTween;
	private readonly Color _targetFadeColor = new Color(.416f, .016f, .059f, 1);
	private Color _fadeColor = new Color(1, 0, 0, 0);
	
	public RoundFailed(StateMachine machine) : base(machine)
	{
		var fade = new FloatTween(x => _fadeColor = new Color(_targetFadeColor.R/255f, _targetFadeColor.G/255f, _targetFadeColor.B/255f, x), 0, 1, 15f);
		_timeoutToGoToTitle = new NopTween(15);
		_fadeScreenTween = new TweenSequence(new NopTween(1f), fade, new NopTween(5f), _timeoutToGoToTitle);
		_fadeScreenTween.Ease = Ease.Linear;
		
		_tween = new IntTween((x => _val = x), 5, 3);
		_scene = new Scene();
		_scene.SetDemon(true);
		_scene.SetMavis(false);
	}

	public override void OnEnter()
	{
		_tween.Reset();
		_fadeScreenTween.Reset();
		Console.WriteLine("round failed");
		Program.GameWindow.SetActiveWindows(Program.GameWindow.FightWindow, Program.GameWindow.TypingWindow);
		Program.GameWindow.FightWindow.SetScene(_scene);
		//AssetManager.Mavis?.Play();
		Program.GoDownALayerOfHell();
		base.OnEnter();
	}

	public override void Tick(float delta)
	{
		// _timeoutToGoToTitle.Tick(delta);//now in the sequence
		_fadeScreenTween.Tick(delta);
		if (!_tween.IsComplete)
		{
			_tween.Tick(delta);
		}

		if (_timeoutToGoToTitle.IsComplete)
		{
			_machine.GoToState(_machine.TitleState);
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

		int fontSize = 64;
		int width = Raylib.GetScreenWidth();
		var y = Program.GameWindow.FightWindow.Height / 2 - (fontSize * 3) / 2;

		if (_fadeColor.A > 0)
		{
			Raylib.DrawRectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), _fadeColor);
		}

		DrawUtility.DrawLineCentered($"The {Demon.Title} {demon.Name} defeated you.", width, y, fontSize, Color.White);
		if (_val > 2)
		{
			DrawUtility.DrawLineCentered("You are banished deeper into hell.", width, y + fontSize*2, fontSize, Color.White);
		}
		if (_val > 4)
		{
			DrawUtility.DrawLineCentered($"Why must you struggle?", width, y + fontSize*4, fontSize, Color.White);
		}
		
	}
}