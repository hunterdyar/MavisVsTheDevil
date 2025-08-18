using System.Numerics;
using Raylib_cs;

namespace MavisVsTheDevil.Panels;

public class GameTitleIdleScreen : PanelBase
{
	private string[] titleLines =
	[
		"You Are Mavis Beacon.",
		"You Are In Hell.",
		"You Must Type."
	];

	private int[] characterCounts;
	private int fontHeight = 48;
	public GameTitleIdleScreen(GameWindow window) : base(window)
	{
		characterCounts = new int[titleLines.Length];
		for (int i = 0; i < titleLines.Length; i++)
		{
			characterCounts[i] = titleLines[i].Length;
		}
	}

	public override void Draw()
	{
		int py = ((Height - (titleLines.Length*fontHeight)/2)/2);
		for (int i = 0; i < titleLines.Length; i++)
		{
			int px = (Width - (characterCounts[i] * fontHeight))/2;
			Raylib.DrawTextEx(Program.terminalFont,titleLines[i],new Vector2(px,py),fontHeight,0, Color.White);
			py += fontHeight;
		}
		
	}
}