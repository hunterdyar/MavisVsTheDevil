namespace MavisVsTheDevil.Engine;

public class TestLetter
{
	private TypeTest _test;
	public readonly char Letter;
	public readonly int Word;
	public LetterState State = LetterState.Waiting;
	public readonly List<char> Mistakes = new List<char>();
	public TestLetter(TypeTest test, char letter, int word)
	{
		_test = test;
		Letter = letter;
		Word = word;
	}

	public bool TryToMatch(char c)
	{
		if (State != LetterState.Current && State != LetterState.Failure)
		{
			throw new Exception("letter should have been set to current!");
		}
		if (Letter == c)
		{
			State = LetterState.Pass;
			return true;
		}
		else
		{
			Mistakes.Add(c);
			State = LetterState.Failure;
			_test.LetterFailure(this);
			return false;
		}
	}

	override public string ToString()
	{
		return Letter.ToString();
	}

	public void SetCurrentSafe()
	{
		if (this.State == LetterState.Waiting)
		{
			this.State = LetterState.Current;
		}
		else
		{
			throw new Exception("The letter is not current, we should go back.");
		}
	}
}