namespace MavisVsTheDevil.Engine;

public class ReplaceLettersWithLetter : Modifier
{
	private char[] _replaceThese;
	private char _withThis;
	private string name;

	public ReplaceLettersWithLetter(string name, char c, params char[] replaceThese)
	{
		this._replaceThese = replaceThese;
		_withThis = c;
		this.name = name;
	}

	public override string GetModifierName()
	{
		return name;
	}

	public override void OnTypingTestCreated(ref TypeTest typeTest)
	{
		foreach (var letter in typeTest.Letters)
		{
			if (_replaceThese.Contains(letter.Letter))
			{
				letter.SetLetter(_withThis);
			}
		}
	}
}