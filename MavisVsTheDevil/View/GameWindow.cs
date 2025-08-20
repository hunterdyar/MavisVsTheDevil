using MavisVsTheDevil.Engine;
using MavisVsTheDevil.GameAnimations;
using MavisVsTheDevil.Panels;
using Raylib_cs;
using static Raylib_cs.Raylib;
namespace MavisVsTheDevil;

//draws the game!
public class GameWindow
{
	public Game Game => _game;
	private readonly Game _game;
	
	public TypingWindow TypingWindow => _typingWindow;
	private readonly TypingWindow _typingWindow;
	public FightWindow FightWindow => _fightWindow;
	private readonly FightWindow _fightWindow;
	public GameTitleIdleScreen TitleIdleScreen => _gameTitleIdle;
	private readonly GameTitleIdleScreen _gameTitleIdle;
	
	public TitleAnim TitleIntroAnim => _titleIntroAnim;
	private readonly TitleAnim  _titleIntroAnim;

	public RoundStartAnimationPanel RoundIntroduction => _roundIntro;
	private readonly RoundStartAnimationPanel _roundIntro;

	public RoundSurvivedAnimationPanel RoundSurvivedAnim => _roundSurvived;
	private readonly RoundSurvivedAnimationPanel _roundSurvived;

	public RoundLostAnimationPanel RoundFailedAnim => _roundFailed;
	private readonly RoundLostAnimationPanel _roundFailed;

	private PanelBase[] _panels;
	
	private readonly DebugInfo _debugInfo;
	public GameWindow(Game game)
	{
		_game = game;
		_fightWindow = new FightWindow(this);
		_typingWindow = new TypingWindow(this);
		_gameTitleIdle =  new GameTitleIdleScreen(this);
		_titleIntroAnim = new TitleAnim(this);
		_roundIntro = new RoundStartAnimationPanel(this);
		_roundSurvived = new RoundSurvivedAnimationPanel(this);
		_roundFailed = new RoundLostAnimationPanel(this);

		_panels = [
			_typingWindow,
			_fightWindow,
			_gameTitleIdle,
			_titleIntroAnim,
			_roundIntro,
			_roundSurvived,
			_roundFailed,
		];
		
		_debugInfo = new DebugInfo(Game);
		SetSizes();
	}

	public void SetActiveWindows(params PanelBase[] panels)
	{
		foreach (PanelBase panel in _panels)
		{
			panel.SetActive(panels.Contains(panel));
		}
	}

	public void Draw()
	{
		foreach (PanelBase panel in _panels)
		{
			if (panel.Enabled)
			{
				panel.Draw();
			}
		}
		//
		_debugInfo.Draw();
	}

	public void SetSizes()
	{
		var w = Raylib.GetScreenWidth();
		var h = Raylib.GetScreenHeight();

		int fightSize = h * 2 / 3;
		_typingWindow.Resize(0,fightSize,w,h-fightSize);
		_fightWindow.Resize(0,0,w,fightSize);
		_gameTitleIdle.Resize(0,0,w,h);
		_titleIntroAnim.Resize(0,0,w,h);
		_roundIntro.Resize(0,0,w,h);
		_roundSurvived.Resize(0,0,w,h);
	}

	public void OnClose()
	{
		foreach (PanelBase panel in _panels)
		{
			panel.OnClose();
		}
	}
}