using MavisVsTheDevil.Animation;

namespace MavisVsTheDevil.Panels;

public class RoundStartAnimationPanel : AnimationPanel
{
	public RoundStartAnimationPanel(GameWindow window) : base(window)
	{
		Primary = new NopTween(0.1f);
	}

	public override void Start()
	{
		base.Start();
	}
	public override void Draw()
	{
		
	}
}