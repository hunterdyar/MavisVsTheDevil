namespace MavisVsTheDevil.Engine;

public abstract class Modifier
{
	public abstract string GetModifierName();
	
	protected virtual void OnRoundCreated(ref Round round)
	{
		
	}

	protected virtual void OnTypingTestCreated(ref TypeTest typeTest)
	{
		
	}

	protected virtual void OnRoundFinished(ref Round round)
	{
		
	}

	protected virtual void OnCorrectLetter(ref TypeTest typeTest, char letter)
	{
		
	}

	protected virtual void OnWrongLetter(ref TypeTest typeTest, char letter)
	{
		
	}

	protected virtual void OnWord(ref TypeTest typeTest, int pos)
	{
		
	}
}