using System.Numerics;
using Raylib_cs;

namespace MavisVsTheDevil.Panels;

public class GameTitleIdleScreen : PanelBase
{
	private readonly string[] _titleLines =
	[
		"You Are Mavis Beacon.",
		"You Are In Hell.",
		"You Must Type."
	];

	private readonly int[] _characterCounts;
	public const int FontHeight = 48;
	public GameTitleIdleScreen(GameWindow window) : base(window)
	{
		_characterCounts = new int[_titleLines.Length];
		for (int i = 0; i < _titleLines.Length; i++)
		{
			_characterCounts[i] = _titleLines[i].Length;
		}
	}

	public override void Draw()
	{
		int py = ((Height - (_titleLines.Length* FontHeight)/2)/2);
		for (int i = 0; i < _titleLines.Length; i++)
		{
			int px = (Width - (_characterCounts[i] * FontHeight))/2;
			DrawUtility.DrawLineCentered(_titleLines[i], Width, py, FontHeight, Color.White, PosX);
			py += FontHeight;
		}
		
	}
}