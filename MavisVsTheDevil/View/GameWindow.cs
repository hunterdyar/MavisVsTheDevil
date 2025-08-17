using MavisVsTheDevil.Engine;
using MavisVsTheDevil.Panels;
using Raylib_cs;
using static Raylib_cs.Raylib;
namespace MavisVsTheDevil;

//draws the game!
public class GameWindow
{
	public Game Game => _game;
	private readonly Game _game;
	
	private readonly TypingWindow _typingWindow;
	private readonly FightWindow _fightWindow;
	public GameWindow(Game game)
	{
		_game = game;
		_fightWindow = new FightWindow(this);
		_typingWindow = new TypingWindow(this);
		SetSizes();
	}

	public void Draw()
	{
		_typingWindow.Draw();
		_fightWindow.Draw();
	}

	public void SetSizes()
	{
		var w = Raylib.GetScreenWidth();
		var h = Raylib.GetScreenHeight();

		int fightSize = h * 2 / 3;
		_typingWindow.Resize(0,fightSize,w,h-fightSize);
		_fightWindow.Resize(0,0,w,fightSize);
	}

	public void OnClose()
	{
		_fightWindow.OnClose();
		_typingWindow.OnClose();
	}
}