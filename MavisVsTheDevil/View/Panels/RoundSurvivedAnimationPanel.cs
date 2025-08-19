using MavisVsTheDevil.Animation;
using MavisVsTheDevil.Engine;
using Raylib_cs;

namespace MavisVsTheDevil.Panels;

public class RoundSurvivedAnimationPanel : AnimationPanel
{
	private int _val;
	public RoundSurvivedAnimationPanel(GameWindow window) : base(window)
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
		
		
		Raylib.DrawText($"You Defeated {demon.Name}.", PosX, PosY+24, 24, Color.White);
		Raylib.DrawText($"The Demon Wails: {demon.Struggle}", PosX, PosY + 68, 24, Color.White); 
	}
}