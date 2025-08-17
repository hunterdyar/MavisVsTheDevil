namespace MavisVsTheDevil.Engine;

public class Round
{
	public readonly int RoundNumber;
	public readonly TypeTest Test;
	public string WordlistName;
	//list Modifiers
	
	public Round(int round)
	{
		RoundNumber = round;
		int wordCount = GetWordCount();
		var w = GetWordlist(RoundNumber);
		WordlistName = w.Item1;
		string[] words = new  string[wordCount];
		for (int i = 0; i < words.Length; i++)
		{
			words[i] = w.Item2[Program.random.Next(w.Item2.Length)];
		}
		Test = new TypeTest(words);
	}

	private int GetWordCount()
	{
		int[] rs = [10, 20, 30, 35,40,40,50,50,50];
		return rs[RoundNumber];
	}

	public static (string, string[]) GetWordlist(int roundNumber)
	{
		if (roundNumber <= 1)
		{
			return ("Common Words", Wordlist.Wordlist.COMMON);
		}else if (roundNumber <= 3)
		{
			var r = Program.random.Next(3);
			switch (r)
			{
				case 0:
					return ("No Home Row", Wordlist.Wordlist.HURDLEHOMEROW);
				case 1:
					return ("Hyphens", Wordlist.Wordlist.HYPHENS);
				case 2:
					return ("Apostrophes", Wordlist.Wordlist.APOSTROPHE);
				}
		}else if (roundNumber <= 5)
		{
			var r = Program.random.Next(6);
			switch (r)
			{
				case 0:
					return ("Left Hand Only", Wordlist.Wordlist.LEFTHAND);
				case 1:
					return ("Left Hand and P", Wordlist.Wordlist.LEFTHAND_P);
				case 2:
					return ("Right Hand Only", Wordlist.Wordlist.RIGHTHAND);
				case 3:
					return ("Tricky", Wordlist.Wordlist.TRICKY);
				case 4:
					return ("Tedious", Wordlist.Wordlist.TRIGRAMS);
				case 5:
					return ("Commonly Misspelled", Wordlist.Wordlist.TYPOD);
			}
		}

		return ("Common Words", Wordlist.Wordlist.COMMON);

	}
}