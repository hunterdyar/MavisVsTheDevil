using static Raylib_cs.Raylib;
namespace MavisVsTheDevil.Engine;

public class Typer
{
	private string _text;
	private int _wpm;
	public Typer()
	{
		
	}

	public void Tick()
	{
		var press = Raylib_cs.Raylib.GetCharPressed();
		while (press != 0)
		{
			char c = (char)press;
			if (char.IsAsciiLetterOrDigit(c) || c == ' ' || c == '\'')
			{
				TypeKeyPressed(c);
			}
			
			press = Raylib_cs.Raylib.GetCharPressed();
		}
	}

	private void TypeKeyPressed(char c)
	{
		//
	}
}