namespace MavisVsTheDevil.Engine;

public class LoseTimeOnMistake(double time) : Modifier
{
	private double _lossPerMistake = time;
	public override string GetModifierName()
	{
		return "Mistakes Cost Time";
	}

	public override void OnWrongLetter(ref TypeTest typeTest, TestLetter letter, char typedLetter)
	{
		typeTest._startTime -= _lossPerMistake;
	}
}