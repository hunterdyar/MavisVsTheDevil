using MavisVsTheDevil.Engine;
using Raylib_cs;

namespace MavisVsTheDevil.Panels;

public class DebugInfo
{
	private int Height = 20;
	private Game game;

	public DebugInfo(Game game)
	{
		this.game = game;
	}
	public void Draw()
	{
		int x = 0;
		Raylib.DrawRectangle(0,0,Raylib.GetScreenWidth(),Height, Color.RayWhite);
		var round = game.CurrentRound;
		var test = game.CurrentTest;

		if (round != null)
		{
			var rn = round.RoundNumber;
			var rs = round.State.ToString();
			Raylib.DrawText($"Round ({rn}): {rs} ", x, 0, 20, Color.Black);
			x += 350;
		}
		else
		{
			Raylib.DrawText("Round: NULL", x,0,20, Color.Black);
			x += 350;
		}

		if (test != null)
		{
			var ts = test.State;
			var tcl = test.CurrentLetter;
			var wx = test.WordCount;
			Raylib.DrawText($"Test: {ts}. Letter: {tcl}. Words: {wx}", x, 0, 20, Color.Black);
			x += 500;
		}
		else
		{
			Raylib.DrawText("Test: NULL", x, 0, 20, Color.Black);
			x += 500;
		}

		var sm = Program.GameWindow.Game.State;
		if (sm != null)
		{
			Raylib.DrawText($"State: {sm.CurrentStateName()}", x, 0, 20, Color.Black);
			x += 500;
		}
		else
		{
			Raylib.DrawText("State: NULL", x, 0, 20, Color.Black);
			x += 200;
		}
	}
}