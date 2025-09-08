namespace MavisVsTheDevil.Engine;

public class MistakesAddedToWord : Modifier
{
	public override string GetModifierName()
	{
		return "Mistakes Added To Word";
	}

	public override void OnWrongLetter(ref TypeTest typeTest, TestLetter letter, char typedLetter)
	{
		typeTest.AppendLetterToWord(typedLetter);
	}
}