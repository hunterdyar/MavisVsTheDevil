using MavisVsTheDevil.Engine;
using Raylib_cs;

namespace MavisVsTheDevil.Panels;

public class FightWindow : PanelBase
{
	public FightWindow(GameWindow gameWindow) : base(gameWindow)
	{
	}

	public override void Draw()
	{
		Raylib.DrawRectangle(PosX, PosY, Width, Height, Color.DarkBlue);
	}
}