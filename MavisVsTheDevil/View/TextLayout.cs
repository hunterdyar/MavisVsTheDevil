using MavisVsTheDevil.Engine;

namespace MavisVsTheDevil;

public class TextLayout
{
    private LetterForm[] layout = [];
    public int MaxLineWidth;

    private int _line = 0;
    private int _lineIndex;
    public int TotalLines;
    public int[] LineWidths = new int[512];
    public TextLayout(int maxLineWidth)
    {
        MaxLineWidth = maxLineWidth;
        TypeTest.OnTestChange += OnTestChange;
    }

    //update self automatically.
    private void OnTestChange(TypeTest test)
    {
        CreateLayout(test);
    }

    private int lastWord = 0;
    public void CreateLayout(TypeTest test)
    {
        if (MaxLineWidth < 1)
        {
            throw new ArgumentException("MaxLineWidth should be > 0");
        }
        layout = new LetterForm[test.Letters.Count];
        _lineIndex = 0;
        _line = 0;
        int currentWidth = 0;
        int widthByWord = 0;
        for (var i = 0; i < test.Letters.Count; i++)
        {
            var letter = test.Letters[i];
            layout[i] = new LetterForm(letter, _line, _lineIndex);
            if (layout[i].BreakAfterAble)
            {
                lastWord = i;
                widthByWord = currentWidth;
            }

            if (currentWidth > MaxLineWidth)
            {
                if (_line > LineWidths.Length)
                {
                    throw new IndexOutOfRangeException("Not enough padding in line widths element.");
                }
                LineWidths[_line] = widthByWord;
                i = lastWord;//roll back the algorithm.
                _line++;
                _lineIndex = 0;
                currentWidth = 0;
            }else
            {
                _lineIndex++;
                currentWidth++;
            }
        }
        LineWidths[_line] = currentWidth;
        TotalLines = _line;
    }

    public void BreakLine()
    {
        
    }

    public LetterForm Get(int index)
    {
        return layout[index];
    }

    public void SetMaxWidth(int maxWidth)
    {
        this.MaxLineWidth = maxWidth;
        var t = Program.GameWindow?.Game?.CurrentRound?.Test;
        if (t != null)
        {
            CreateLayout(t);
        }
    }
}

public struct LetterForm
{
    public TestLetter Letter;
    public int Width = 1;
    public bool BreakAfterAble;
    public int Line = 0;
    public int Column = 0;

    public LetterForm(TestLetter letter)
    {
        Letter = letter;
        BreakAfterAble = false;
        BreakAfterAble = CanBreakAfter(letter.Letter);
    }
    public LetterForm(TestLetter letter, int line, int column)
    {
        Letter = letter;
        BreakAfterAble = false;
        BreakAfterAble = CanBreakAfter(letter.Letter);
        Line = line;
        Column = column;
    }
    

    private bool CanBreakAfter(char c)
    {
        return c == ' ' || c == '_' || c == '\n';
    }
}