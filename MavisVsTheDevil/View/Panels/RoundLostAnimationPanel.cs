using MavisVsTheDevil.Animation;
using Raylib_cs;

namespace MavisVsTheDevil.Panels;

public class RoundLostAnimationPanel : AnimationPanel
{
	private int _val;

	public RoundLostAnimationPanel(GameWindow window) : base(window)
	{
		Primary = new IntTween((x => _val = x), 5, 3);
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
		Raylib.DrawText($"The demon {demon.Name} defeated you.", x, y + 24, 24, Color.White);
		Raylib.DrawText($"You are banished deeper into hell.", x, y + 34, 24, Color.White);
		Raylib.DrawText($"Why must you struggle?", x, y + 68, 24, Color.White);
	}
}