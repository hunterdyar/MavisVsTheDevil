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

		var x = PosX + 50;
		var y = PosY + 50;
		DrawUtility.DrawLineCentered($"You have defeated the demon {demon.Name}.", Width, y+24, 24, Color.White, PosX);
		DrawUtility.DrawLineCentered($"{demon.Name} is banished deeper into hell.", Width, y + 46, 24, Color.White, PosX);
		DrawUtility.DrawLineCentered($"The Demon Wails: {demon.Struggle}", Width, y + 68, 24, Color.White,  PosX); 
	}
}