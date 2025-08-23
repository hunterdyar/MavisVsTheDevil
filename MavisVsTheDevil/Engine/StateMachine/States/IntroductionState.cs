using System.Numerics;
using MavisVsTheDevil.Animation;
using MavisVsTheDevil.Panels;
using Raylib_cs;

namespace MavisVsTheDevil.Engine;

public class IntroductionState : StateBase
{
	public TweenSequence _sequence;
	private string prefix = "You Have Decided ";

	private string[] suffix =
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

	private IndexTween<string> _suffixAnim;
	private ColorTween _fadeOutAnim;
	private string text;
	private Color _color = Color.White;
	
	private PanelBase[] AllActivePanels;

	private int fontSize = 32;

	private int Width = Raylib.GetScreenWidth();

	private int text_x;
	//on skip input or on animationComplete, go to determined state
	public IntroductionState(StateMachine machine) : base(machine)
	{
		_suffixAnim = new IndexTween<string>((v) =>
		{
			text = prefix + v;
			_color = Color.White;
		}, suffix, 2);
		_fadeOutAnim = new ColorTween((c) => { _color = c; }, Color.White, new Color(255, 255, 255, 0), 0.25f);
		_sequence = new TweenSequence(_suffixAnim, _fadeOutAnim, new NopTween(0.5f));
		//
		var tx = Raylib.MeasureTextEx(Program.terminalFont, text, fontSize, 0);
		int x = (int)(Width - tx.X) / 2;
	}
	
	public override void OnEnter()
	{
		_sequence.Reset();
		Console.WriteLine("enter animation state");
		//we subscribe and ubsubscribe from the action so that the animations can be reused by multiple states, jic.
		// _animPanel.Primary.OnComplete += OnAnimComplete;
		Program.GameWindow.SetActiveWindows([]);
		Width = Raylib.GetScreenWidth();
		var tx = Raylib.MeasureTextEx(Program.terminalFont, prefix+suffix[0], fontSize, 0);
		text_x = (int)(Width - tx.X) / 2;
		
		base.OnEnter();
	}

	private void OnAnimComplete()
	{
		_machine.GoToState(_machine.RoundStartAnimation);
	}

	public override void Tick(float delta)
	{
		//it's sort of an anti-pattern to stop this here and not internally, but i have some console.writes i want to supress soooooo
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
		int Height = Raylib.GetScreenHeight();
		Raylib.DrawTextEx(Program.terminalFont, text, new Vector2(text_x, Height / 2 + 16), fontSize, 0, _color);

	}
}