namespace MavisVsTheDevil.Engine;

public class ReverseCapitalization : Modifier
{
	public override string GetModifierName()
	{
		return "Flip Upper and Lowercase";
	}

	public override void OnTypingTestCreated(ref TypeTest typeTest)
	{
		foreach (var letter in typeTest.Letters)
		{
			letter.SetLetter(ModifierUtility.FlipCase(letter.Letter));
		}
	}
	// public override char OnLetterTyped(char c)
	// {
	// 	if (char.IsUpper(c))
	// 	{
	// 		return char.ToLower(c);
	// 	}else if (char.IsLower(c))
	// 	{
	// 		return char.ToUpper(c);
	// 	}
	//
	// 	return c;
	// }
}