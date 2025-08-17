using static Raylib_cs.Raylib;
namespace MavisVsTheDevil.Engine;

public enum TypeTestState
{
	WaitingToStart,
	Typing,
	Finished,
	Idle,//do nothing, wait for someone else to tell you to wait for input.
}

public class TypeTest
{
	
	//todo: on constructor, turn this into a list of LETTERATTEMPTS, and we run through character by character not word by word.
	
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

	//used for devil modifier "only words you suck at"
	public List<string> _failedWords = new List<string>();
	public TypeTest(string[] words)
	{
		_words = words;
		_testLetters.Clear();
		for (var w = 0; w < words.Length; w++)
		{
			string word = words[w];
			foreach (char c in word)
			{
				_testLetters.Add(new TestLetter(this,c,w));
			}
			//space
			if (w < words.Length - 1)
			{
				_testLetters.Add(new TestLetter(this, ' ', w));
			}
		}
		WordCount = _words.Length;
		_rawInput.Clear();
		_state = TypeTestState.WaitingToStart;
	}

	public void Reset()
	{
		_rawInput.Clear();
		_state = TypeTestState.WaitingToStart;
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
			_testLetters[0].SetCurrentSafe();
		}else if (_state == TypeTestState.Finished)
		{
			return;
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
			}
			else
			{
				_testLetters[_currentLetter].SetCurrentSafe();
			}
		}
	}

	public void LetterFailure(TestLetter letter)
	{
		var failedWord = _words[letter.Word];
		if (!_failedWords.Contains(failedWord))
		{
			_failedWords.Add(failedWord);
		}
	}
}