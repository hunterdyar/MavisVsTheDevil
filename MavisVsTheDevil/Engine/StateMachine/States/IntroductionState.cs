using System.Numerics;
using MavisVsTheDevil.Animation;
using MavisVsTheDevil.Panels;
using Raylib_cs;

namespace MavisVsTheDevil.Engine;

public class IntroductionState : StateBase
{
	private readonly TweenSequence _sequence;
	private const string Prefix = "You Have Decided ";

	private readonly string[] _suffix =
	[
		"To Suffer",
		"To Reist",
		"To Type",
		"To Struggle",
		"To Fight",
		"To Type",
		"To Despair",
		"To Type",
		"To Struggle",
		"To Type",
		"Wrong",
	];

	private string _text;
	private Color _color = Color.White;

	private const int FontSize = 32;

	private int _textX;
	public IntroductionState(StateMachine machine) : base(machine)
	{
		var suffixAnim = new IndexTween<string>((v) =>
		{
			_text = Prefix + v;
			_color = Color.White;
		}, _suffix, 2);
		var fadeOutAnim = new ColorTween((c) => { _color = c; }, Color.White, new Color(255, 255, 255, 0), 0.25f);
		_sequence = new TweenSequence(suffixAnim, fadeOutAnim, new NopTween(0.5f));
		_textX = 0;
		_text = "";
	}
	
	public override void OnEnter()
	{
		_sequence.Reset();
		Program.GameWindow.SetActiveWindows([]);
		var tx = Raylib.MeasureTextEx(Program.terminalFont, Prefix+_suffix[0], FontSize, 0);
		_textX = (int)(Program.GameWindow.FightWindow.Width- tx.X) / 2;
		base.OnEnter();
	}

	private void OnAnimComplete()
	{
		_machine.GoToState(_machine.RoundStartAnimation);
	}

	public override void Tick(float delta)
	{
		if (!_sequence.IsComplete)
		{
			_sequence.Tick(delta);
		}

		if (_sequence.IsComplete)
		{
			OnAnimComplete();
		}
		
		base.Tick(delta);
	}

	public override void Draw()
	{
		int height = Raylib.GetScreenHeight();
		Raylib.DrawTextEx(Program.terminalFont, _text, new Vector2(_textX, height / 2 + 16), FontSize, 0, _color);
	}
}