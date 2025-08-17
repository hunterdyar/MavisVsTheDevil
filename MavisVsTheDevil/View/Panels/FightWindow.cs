using MavisVsTheDevil.Engine;
using Raylib_cs;

namespace MavisVsTheDevil.Panels;

public class FightWindow : PanelBase
{
	private GameWindow _gameWindow;
	public FightWindow(GameWindow gameWindow)
	{
		_gameWindow = gameWindow;
	}

	public override void Draw()
	{
		Raylib.DrawRectangle(PosX, PosY, Width, Height, Color.DarkBlue);
	}
}