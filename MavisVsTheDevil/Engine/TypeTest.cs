using Raylib_cs;
using static Raylib_cs.Raylib;
namespace MavisVsTheDevil.Engine;

public enum TypeTestState
{
	WaitingToStart,
	Typing,
	Finished,
	OutOfTime,
	Idle,//do nothing, wait for someone else to tell you to wait for input.
}

public class TypeTest
{
	
	//todo: on constructor, turn this into a list of LETTERATTEMPTS, and we run through character by character not word by word.
	
	public static Action<TypeTestState> OnStateChange;
	public List<TestLetter> Letters => _testLetters;
	private List<TestLetter> _testLetters = new List<TestLetter>();
	public int WordCount { get; }
	private readonly List<char> _rawInput  = new List<char>();
	public char[] RawInput() => _rawInput.ToArray();
	public int CurrentLetter => _currentLetter;
	private int _currentLetter = 0;
	public string[] Words => _words;
	private string[] _words;
	public TypeTestState State => _state;
	private TypeTestState _state;
	public int[] LetterCountByWordIndex;

	public double Elapsed => (_state == TypeTestState.Typing) ? Raylib.GetTime() - _startTime : 0;
	public double _startTime;
	public double _allowedTime;

	public Modifier[] Modifiers = [];


	//used for devil modifier "only words you suck at"
	public List<string> _failedWords = new List<string>();
	private Round _round;
	public TypeTest(Round round, string[] words, Modifier[] modifiers, double allowedTime)
	{
		_round = round;
		_words = words;
		Modifiers = modifiers;
		_allowedTime = allowedTime;
		
		_testLetters.Clear();
		LetterCountByWordIndex = new int[_words.Length];
		for (var w = 0; w < words.Length; w++)
		{
			string word = words[w];
			foreach (char c in word)
			{
				_testLetters.Add(new TestLetter(this,c,w));
			}

			if (w == 0)
			{
				LetterCountByWordIndex[w] = words.Length;
			}
			else
			{
				LetterCountByWordIndex[w] = LetterCountByWordIndex[w - 1] + word.Length;
			} //space

			if (w < words.Length - 1)
			{
				_testLetters.Add(new TestLetter(this, ' ', w));
				LetterCountByWordIndex[w]++;
			}
		}
		WordCount = _words.Length;
		_rawInput.Clear();
		_state = TypeTestState.Idle;

		foreach (var modifier in Modifiers)
		{
			var test = this;
			//these can be one thing I guess.
			modifier.OnTypingTestCreated(ref test);
		}
		
		
		OnStateChange?.Invoke(TypeTestState.Idle);
		
	}

	public void Reset()
	{
		_rawInput.Clear();
		_state = TypeTestState.WaitingToStart;
		OnStateChange.Invoke(TypeTestState.WaitingToStart);
		_failedWords.Clear();
		foreach (TestLetter testLetter in _testLetters)
		{
			testLetter.State = LetterState.Waiting;
		}
		_currentLetter = 0;
	}

	public void TypeKeyPressed(char c)
	{
		//Start imer
		if (_state == TypeTestState.WaitingToStart)
		{
			_state = TypeTestState.Typing;
			_startTime = Raylib.GetTime();
			OnStateChange.Invoke(TypeTestState.Typing);
			_testLetters[0].SetCurrentSafe();
		}else if (_state == TypeTestState.Finished)
		{
			return;
		}

		//dev hacks cheat2win4lyfe
		if (c == '=')
		{
			foreach (TestLetter testLetter in _testLetters)
			{
				testLetter.State = LetterState.Pass;
			}

			_state = TypeTestState.Finished;
			OnStateChange.Invoke(TypeTestState.Finished);
			return;
		}

		foreach (var modifier in Modifiers)
		{
			c = modifier.OnLetterTyped(c);
		}
		
		_rawInput.Add(c);
		TestCurrentLetter(c);
	}

	private void TestCurrentLetter(char c)
	{
		var current =  _testLetters[_currentLetter];
		var test = current.TryToMatch(c);
		//fire off success/failure events for particles and such.
		if (test)
		{
			_currentLetter++;
			if (_currentLetter >= _testLetters.Count)
			{
				//DONE
				_state = TypeTestState.Finished;
				OnStateChange.Invoke(TypeTestState.Finished);
				
			}
			else
			{
				_testLetters[_currentLetter].SetCurrentSafe();
			}
		}
	}

	public void LetterFailure(TestLetter letter, char typedLetter)
	{
		var failedWord = _words[letter.Word];
		if (!_failedWords.Contains(failedWord))
		{
			_failedWords.Add(failedWord);
		}

		var test = this;
		foreach (var modifier in Modifiers)
		{
			modifier.OnWrongLetter(ref test, letter, typedLetter);
		}
	}

	public void LetterPass(TestLetter testLetter)
	{
		var test = this;
		foreach (var modifier in Modifiers)
		{
			modifier.OnCorrectLetter(ref test, testLetter);
		}
	}

	public void SetWaitForPlayer()
	{
		if (_state == TypeTestState.Idle)
		{
			_state = TypeTestState.WaitingToStart;
			OnStateChange.Invoke(TypeTestState.WaitingToStart);
		}
		else
		{
			throw new Exception("invalid test state transition");
		}
	}

	private int firstXWordsTestCache = -1;
	private int firstXWordsTestCacheResult = 0;
	public int GetLetterCountForFirstNumberWords(int firstWord, int lastWord)
	{
		if (firstWord < 0 || lastWord < 0)
		{
			return 0;
		}

		if (firstWord > WordCount - 1 || lastWord > WordCount - 1)
		{
			return 0;
		}

		return LetterCountByWordIndex[lastWord] - LetterCountByWordIndex[firstWord];
	}

	public void SetTestLetters(List<TestLetter> newTest)
	{
		_testLetters = newTest;
	}

	public void AppendLetterToTest(char letter, bool newWord)
	{
		if (newWord)
		{
			throw new NotImplementedException();
		}
		//if it's a space, we'll bork word breaksssss. soooo. gotta fix that.
		_testLetters.Add(new TestLetter(this, letter, WordCount-1));
		LetterCountByWordIndex[^1]++;
	}


	public void Tick()
	{
		if (_state == TypeTestState.Typing)
		{
			var t = Raylib.GetTime();
			var elapsed = t - _startTime;
			if (elapsed > _allowedTime)
			{
				_state = TypeTestState.OutOfTime;
				OnStateChange?.Invoke(_state);
			}
	}
	}
}