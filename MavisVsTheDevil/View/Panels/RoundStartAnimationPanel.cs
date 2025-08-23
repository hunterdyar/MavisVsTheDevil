using MavisVsTheDevil.Animation;
using MavisVsTheDevil.Elements;
using MavisVsTheDevil.Engine;
using Raylib_cs;

namespace MavisVsTheDevil.Panels;

public class RoundStartAnimationPanel : AnimationPanel
{
	private int _val;
	private Scene _scene;
	
	public RoundStartAnimationPanel(GameWindow window) : base(window)
	{
		Primary = new IntTween((x=>_val=x),5, 3);
		_scene = new Scene();
		_scene.SetDemon(true);
	}

	public override void Start()
	{
		_val = 0;
		
		_window.FightWindow.SetScene(_scene);
		
		//hate this... this has to live somewhere else :/
		AssetManager.Demon.StopAndResetAnim();

		base.Start();
	}
	public override void Draw()
	{
		var demon = _window.Game.CurrentRound?.Demon;
		if (demon == null)
		{
			return;
		}
		
		
		DrawUtility.DrawLineCentered($"You Encounter {demon.Name}.", Width, PosY+24, 24, Color.White);
		if (_val < 1)
		{
			return;
		}
		DrawUtility.DrawLineCentered($"Cause of death: {demon.CauseOfDeath}.", Width, PosY+44, 24, Color.White);
		if (_val < 2)
		{
			return;
		}
		DrawUtility.DrawLineCentered("It gives you modifiers: ...", Width, PosY + 88, 24, Color.White);

		DrawUtility.DrawLineCentered("... ready? ... ", Width, PosY + 108, 12, Color.White);

	}
}