namespace MavisVsTheDevil.Engine;

public class EveryLetterTwice : Modifier
{

	public EveryLetterTwice()
	{
	}

	public override string GetModifierName()
	{
		return "Random Letters Capitalized";
	}

	public override void OnTypingTestCreated(ref TypeTest typeTest)
	{
		var newTest = new List<TestLetter>();
		foreach (var letter in typeTest.Letters)
		{
			newTest.Add(letter);
			if (char.IsLetterOrDigit(letter.Letter))
			{
				newTest.Add(letter.Clone());
			}
		}

		typeTest.SetTestLetters(newTest);
	}
}