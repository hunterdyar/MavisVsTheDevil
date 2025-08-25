using System.Text;

namespace MavisVsTheDevil.Engine;

public static class ModifierUtility
{
	public static char FlipCase(char letter)
	{
		if (char.IsUpper(letter))
		{
			return char.ToLowerInvariant(letter);
		}if (char.IsLower(letter))
		{
			return char.ToUpperInvariant(letter);
		}
		else
		{
			//' or " or such.
			return letter;
		}
	}

	public static string GetModifierNames(Modifier[] modifiers)
	{
		var count = modifiers.Length;
		if (count == 1)
		{
			return modifiers[0].GetModifierName();
		}
		StringBuilder _sb = new StringBuilder();

		for (int i = 0; i < count; i++)
		{
			_sb.Append(modifiers[i].GetModifierName());
			if (i < count - 1)
			{
				_sb.Append(", ");
			}
		}

		return _sb.ToString();
	}
}