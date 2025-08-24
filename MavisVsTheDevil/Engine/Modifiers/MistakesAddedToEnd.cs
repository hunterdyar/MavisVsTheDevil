namespace MavisVsTheDevil.Engine;

public class MistakesAddedToEnd :Modifier
{
	public override string GetModifierName()
	{
		return "Mistakes Added To Test";
	}

	public override void OnWrongLetter(ref TypeTest typeTest, TestLetter letter, char typedLetter)
	{
		typeTest.AppendLetterToTest(typedLetter, false);
	}
}