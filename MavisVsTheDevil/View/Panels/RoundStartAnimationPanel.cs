using MavisVsTheDevil.Animation;
using MavisVsTheDevil.Engine;
using Raylib_cs;

namespace MavisVsTheDevil.Panels;

public class RoundStartAnimationPanel : AnimationPanel
{
	private int _val;
	public RoundStartAnimationPanel(GameWindow window) : base(window)
	{
		Primary = new IntTween((x=>_val=x),5, 3);
	}

	public override void Start()
	{
		_val = 0;
		base.Start();
	}
	public override void Draw()
	{
		var demon = _window.Game.CurrentRound?.Demon;
		if (demon == null)
		{
			return;
		}
		
		
		Raylib.DrawText($"You Encounter {demon.Name}.", PosX, PosY+24, 24, Color.White);
		if (_val < 1)
		{
			return;
		}
		Raylib.DrawText($"Cause of death: {demon.CauseOfDeath}.", PosX, PosY+44, 24, Color.White);
		if (_val < 2)
		{
			return;
		}
		Raylib.DrawText("It gives you modifiers: ...", PosX, PosY + 88, 24, Color.White);

		Raylib.DrawText("... ready? ... ", PosX, PosY + 108, 12, Color.White);

	}
}