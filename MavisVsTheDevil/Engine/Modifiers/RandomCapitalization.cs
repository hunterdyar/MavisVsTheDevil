namespace MavisVsTheDevil.Engine;

public class RandomCapitalization : Modifier
{
	private float _percentReversed = 0.2f;

	public RandomCapitalization(float percent)
	{
		_percentReversed = percent;
	}
	public override string GetModifierName()
	{
		return "Random Letters Capitalized";
	}

	public override void OnTypingTestCreated(ref TypeTest typeTest)
	{
		foreach (var letter in typeTest.Letters)
		{
			if (Program.random.NextSingle() <= _percentReversed)
			{
				letter.SetLetter(ModifierUtility.FlipCase(letter.Letter));
			}
		}
	}
}