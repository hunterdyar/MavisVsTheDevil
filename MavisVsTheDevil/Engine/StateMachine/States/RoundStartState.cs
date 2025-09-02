using MavisVsTheDevil.Animation;
using MavisVsTheDevil.Elements;
using Raylib_cs;

namespace MavisVsTheDevil.Engine;

public class RoundStartState : StateBase
{
	private int _textVal;
	private readonly Scene _scene;
	private string _modifiers = "";
	private BannerText _demonNameBanner;
	private BannerText _causeOfDeathBanner;
	private BannerText _wordListBanner;
	private BannerText _modifierBanner;
	public RoundStartState(StateMachine machine) : base(machine)
	{
		_scene = new Scene();
		_scene.SetDemon(true);
	}

	public override void OnEnter()
	{
		Program.GameWindow.Game.StartNewRound();
		Program.GameWindow.SetActiveWindows(Program.GameWindow.FightWindow, Program.GameWindow.TypingWindow);
		Program.GameWindow.FightWindow.SetScene(_scene);
		AssetManager.Demon.StopAndResetAnim();
		var bannerHeight = Raylib.GetScreenHeight() / 7;
		var bannerGap = 10;
		//demon name
		_demonNameBanner = new BannerText(bannerHeight, bannerHeight,
			"You meet the demon", false, _machine.Game.CurrentRound.Demon.Name);
		//, $"Cause of death: {_machine.Game.CurrentRound.Demon.CauseOfDeath}

		_causeOfDeathBanner = new BannerText(bannerHeight, bannerHeight * 2 + bannerGap, "Cause Of Death", false,
			_machine.Game.CurrentRound.Demon.CauseOfDeath);
		
		_wordListBanner = new BannerText(bannerHeight, bannerHeight*3+bannerGap*2,
			"Word List", false, _machine.Game.CurrentRound.WordlistName);
		
		//modifier banner
		_modifiers = ModifierUtility.GetModifierNames(_machine.Game.CurrentRound.Test.Modifiers);
		if (_machine.Game.CurrentRound.Test.Modifiers.Length == 0)
		{
			_modifierBanner = new BannerText(bannerHeight, bannerHeight*4+bannerGap*3, "Modifiers:",false, "None");
		}
		else
		{
			_modifierBanner = new BannerText(bannerHeight, bannerHeight*4+bannerGap*3, "Modifiers:",false, _machine.Game.CurrentRound.Test.Modifiers.Select(x=>x.GetModifierName()).ToArray());
		}

		Program.GameWindow.TypingWindow.SetTextOpacity(0.5f);
		base.OnEnter();
	}

	public override void Tick(float delta)
	{
		if (!_demonNameBanner.IsComplete)
		{
			_demonNameBanner.Tick(delta);
			return;
		}

		if (!_causeOfDeathBanner.IsComplete)
		{
			_causeOfDeathBanner.Tick(delta);
			return;
		}

		if (!_wordListBanner.IsComplete)
		{
			_wordListBanner.Tick(delta);
			return;
		}

		if (!_modifierBanner.IsComplete)
		{
			_modifierBanner.Tick(delta);
			return;
		}

		if (!_modifierBanner.ExitTween.IsComplete)
		{
			_modifierBanner.ExitTween.Tick(delta);
		}

		
		if (_modifierBanner.ExitTween.PercentageComplete > 0.5f &&
		    !_wordListBanner.ExitTween.IsComplete)
		{
			_wordListBanner.ExitTween.Tick(delta);
		}

		if (_modifierBanner.ExitTween.PercentageComplete > 0.5f &&
		    _wordListBanner.ExitTween.PercentageComplete > 0.5f &&
		    !_causeOfDeathBanner.ExitTween.IsComplete)
		{
			_causeOfDeathBanner.ExitTween.Tick(delta);
		}
		
		if (_modifierBanner.ExitTween.PercentageComplete > 0.5f &&
		    _causeOfDeathBanner.ExitTween.PercentageComplete > 0.5f &&
		    _wordListBanner.ExitTween.PercentageComplete > 0.5f && !_demonNameBanner.ExitTween.IsComplete)
		{
			_demonNameBanner.ExitTween.Tick(delta);
		}
		
		//done
		if (_modifierBanner.IsComplete && _wordListBanner.ExitTween.IsComplete && _demonNameBanner.ExitTween.IsComplete && _causeOfDeathBanner.ExitTween.IsComplete)
		{
			_machine.GoToState(_machine.TypeGameplay);
		}

	}

	public override void TypeKeyPressed(char key)
	{
		// if (_tween.IsComplete)
		// {
		// 	_machine.GoToState(_machine.TypeGameplay);
		// }	
	}

	public override void Draw()
	{
		_demonNameBanner.Draw();
		_causeOfDeathBanner.Draw();
		_wordListBanner.Draw();
		_modifierBanner.Draw();
		var demon = _machine.Game.CurrentRound?.Demon;
		if (demon == null)
		{
			return; 
		}
		int width = Raylib.GetScreenWidth();
		int posY = Raylib.GetScreenHeight()/2;

		// DrawUtility.DrawLineCentered($"You Encounter {demon.Name}.", width, posY + 24, 24, Color.White);
		// if (_textVal < 1)
		// {
		// 	return;
		// }

		// DrawUtility.DrawLineCentered($"Cause of death: {demon.CauseOfDeath}.", width, posY + 44, 24, Color.White);
		// if (_textVal < 2)
		// {
		// 	return;
		// }

		//DrawUtility.DrawLineCentered($"It gives you modifiers: {_modifiers}", width, posY + 88, 24, Color.White);

		//DrawUtility.DrawLineCentered("... ready? ... ", width, posY + 128, 38, Color.White);

	}
}