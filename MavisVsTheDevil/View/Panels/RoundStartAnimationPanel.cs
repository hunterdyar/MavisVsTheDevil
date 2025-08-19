using MavisVsTheDevil.Animation;
using Raylib_cs;

namespace MavisVsTheDevil.Panels;

public class RoundStartAnimationPanel : AnimationPanel
{
	public RoundStartAnimationPanel(GameWindow window) : base(window)
	{
		Primary = new NopTween(1f);
	}

	public override void Start()
	{
		base.Start();
	}
	public override void Draw()
	{
		Raylib.DrawText("You Encounter Zaboomafoo.", PosX, PosY, 24, Color.White);
		Raylib.DrawText("Cause of death: Starvation.", PosX, PosY+24, 24, Color.White);
		Raylib.DrawText("It gives you wordlist: ...", PosX, PosY + 48, 24, Color.White);
		Raylib.DrawText("It gives you modifiers: ...", PosX, PosY + 68, 24, Color.White);
	}
}