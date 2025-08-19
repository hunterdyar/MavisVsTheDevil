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
		Raylib.DrawText($"You have defeated the demon {demon.Name}.", x, y+24, 24, Color.White);
		Raylib.DrawText($"{demon.Name} is banished deeper into hell.", x, y + 34, 24, Color.White);
		Raylib.DrawText($"The Demon Wails: {demon.Struggle}", x, y + 68, 24, Color.White); 
	}
}