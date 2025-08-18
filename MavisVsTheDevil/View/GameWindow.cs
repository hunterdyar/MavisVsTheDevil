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
	
	private readonly DebugInfo _debugInfo;
	public GameWindow(Game game)
	{
		_game = game;
		_fightWindow = new FightWindow(this);
		_typingWindow = new TypingWindow(this);
		_gameTitleIdle =  new GameTitleIdleScreen(this);
		_titleIntroAnim = new TitleAnim(this);
		_debugInfo = new DebugInfo(Game);
		SetSizes();
	}

	public void SetActiveWindows(params PanelBase[] panels)
	{
		//there is probably a better way to intersect two lists :p
		_typingWindow.SetActive(panels.Contains(_typingWindow));
		_fightWindow.SetActive(panels.Contains(_fightWindow));
		_gameTitleIdle.SetActive(panels.Contains(_gameTitleIdle));
		_titleIntroAnim.SetActive(panels.Contains(_titleIntroAnim));
	}

	public void Draw()
	{
		if (_gameTitleIdle.Enabled)
		{
			_gameTitleIdle.Draw();
		}
		
		if (_typingWindow.Enabled)
		{
			_typingWindow.Draw();
		}

		if (_fightWindow.Enabled)
		{
			_fightWindow.Draw();
		}

		if (_titleIntroAnim.Enabled)
		{
			_titleIntroAnim.Draw();
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

	}

	public void OnClose()
	{
		_fightWindow.OnClose();
		_typingWindow.OnClose();
	}
}