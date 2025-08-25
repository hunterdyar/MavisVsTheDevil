namespace MavisVsTheDevil.Engine;

public class SwapLetters : Modifier
{
	private char _a;
	private char _b;

	public SwapLetters(char a, char b)
	{
		_a = a;
		_b = b;
	}

	public override string GetModifierName()
	{
		return $"Swap {_a} and {_b}";
	}

	public override char OnLetterTyped(char c)
	{
		if (c == char.ToUpper(_a))
		{
			return char.ToUpper(_b);
		}

		if (c == char.ToLower(_a))
		{
			return char.ToLower(_b);
		}

		if (c == char.ToUpper(_b))
		{
			return char.ToUpper(_a);
		}

		if (c == char.ToLower(_b))
		{
			return char.ToLower(_a);
		}

		return c;
	}
}