namespace MavisVsTheDevil.Engine;

public abstract class Modifier
{
	private static Modifier[] _recentModifiers = new Modifier[3];
	private static int _recentIndex = 0;
	public static Modifier[] GetModifierForRound(int round)
	{
		if (round == 0)
		{
			//no modifier on first round
			return [];
		}
		else if (round < 3)
		{
			return [GetRandomNoRecent()];
		}
		else
		{
			return [GetRandomNoRecent(), GetRandomNoRecent()];
		}
	}

	private static Modifier GetRandomPure()
	{
		return _allModifiersNoWeighting[Program.random.Next(0, _allModifiersNoWeighting.Length)];
	}

	private static Modifier GetRandomNoRecent()
	{
		var x = GetRandomPure();
		int escape = 0;
		while (_recentModifiers.Contains(x) && escape < 100)
		{
			x = GetRandomPure();
			escape++;
		}

		_recentModifiers[_recentIndex] = x;
		_recentIndex = (_recentIndex + 1) % _recentModifiers.Length;
		
		return x;
	}

	
	private static Modifier mistakesToEndOfTest = new MistakesAddedToEnd();
	private static Modifier everyLetterTwice = new EveryLetterTwice();
	private static Modifier randomCap20 = new RandomCapitalization(.2f);
	private static Modifier randomCap50 = new RandomCapitalization(.5f);
	private static Modifier vowelsWithO = new ReplaceLettersWithLetter("Replace Vowels With o",'o', 'a', 'e', 'i', 'u');

	private static Modifier swampMN = new SwapLetters('m', 'n');
	private static Modifier swampFJ = new SwapLetters('f', 'j');
	private static Modifier reverseCaps = new ReverseCapitalization();

	private static Modifier[] _allModifiersNoWeighting = new[]
	{
		mistakesToEndOfTest,
		everyLetterTwice,
		randomCap20,
		randomCap50,
		vowelsWithO,
		swampMN,
		swampFJ,
		reverseCaps
	};
	
	public abstract string GetModifierName();

	public virtual void OnTypingTestCreated(ref TypeTest typeTest)
	{
		
	}

	public virtual void OnRoundFinished(ref Round round)
	{
		
	}

	public virtual char OnLetterTyped(char c)
	{
		return c;
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