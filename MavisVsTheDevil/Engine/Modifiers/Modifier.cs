namespace MavisVsTheDevil.Engine;

public abstract class Modifier
{
	public abstract string GetModifierName();
	
	public virtual void OnRoundCreated(ref Round round)
	{
		
	}

	public virtual void OnTypingTestCreated(ref TypeTest typeTest)
	{
		
	}

	public virtual void OnRoundFinished(ref Round round)
	{
		
	}

	public virtual void OnCorrectLetter(ref TypeTest typeTest, TestLetter letter)
	{
		
	}

	public virtual void OnWrongLetter(ref TypeTest typeTest, TestLetter letter, char typedLetter)
	{
		
	}

	public virtual void OnWord(ref TypeTest typeTest, int pos)
	{
		
	}
}