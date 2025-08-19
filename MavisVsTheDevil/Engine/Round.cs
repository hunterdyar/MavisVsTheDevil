using MavisVsTheDevil.Demons;
using Raylib_cs;

namespace MavisVsTheDevil.Engine;

public enum RoundState
{
	AnimatingBeforeStart,
	WaitingForPlayerToStart,
	Typing,
	Complete,
	Failure,
}
public class Round
{
	public Demon Demon;
	public static Action<RoundState> OnRoundStateChanged;
	public readonly int RoundNumber;
	public readonly TypeTest Test;
	public string WordlistName;
	public RoundState State => _state;

	private RoundState _state;
	//list Modifiers
	
	public Round(int round)
	{
		RoundNumber = round;
		this._state = RoundState.AnimatingBeforeStart;
		Demon = Demon.GetRandomDemon();
		int wordCount = GetWordCount();
		var w = GetWordlist(RoundNumber);
		WordlistName = w.Item1;
		string[] words = new  string[wordCount];
		for (int i = 0; i < words.Length; i++)
		{
			words[i] = w.Item2[Program.random.Next(w.Item2.Length)];
		}
		Test = new TypeTest(words);
		TypeTest.OnStateChange += OnTypeTestStatechange;
	}

	private void OnTypeTestStatechange(TypeTestState test)
	{
		if (_state == RoundState.WaitingForPlayerToStart)
		{
			//we just changed to typing (from idle, presumably)
			if (test == TypeTestState.Typing)
			{
				ChangeState(RoundState.Typing);
			}
		}else if (_state == RoundState.Typing)
		{
			if (test == TypeTestState.Finished)
			{
				ChangeState(RoundState.Complete);
			}
		}
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
		else
		{
			return ("DESPAIR", Wordlist.Wordlist.DESPAIR);
		}

		return ("Common Words", Wordlist.Wordlist.COMMON);
	}

	public void Start()
	{
		Test.SetWaitForPlayer();
		ChangeState(RoundState.WaitingForPlayerToStart);
	}

	private void ChangeState(RoundState roundState)
	{
		_state = roundState;
		OnRoundStateChanged?.Invoke(_state);
	}
}