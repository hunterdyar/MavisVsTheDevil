using System.Diagnostics;
using System.Numerics;
using MavisVsTheDevil.Animation;
using Raylib_cs;

namespace MavisVsTheDevil.Elements;

public class BannerText
{
    //BannerText goes text... woosh
    public string _bannerTitle;
    public string[] _bannerText;
    public bool IsComplete => textAnim.IsComplete;
    private int count = 0;
    private TweenSequence textAnim;
    private int _xPos;
    private int[] _textDrawXPos;
    private int[] _textPadXPos;
    private int[] _fontSizes;
    private int _height;
    private int _width;
    private int _y;
    private int _currentTextEntering = -1;
    public Color Background = Color.Red;
    public Color Foreground = Color.Black;

    public TweenBase ExitTween;
    //todo: xPadding to center the text.
    public BannerText(int height,int y, string title, bool includeExitInSequence, params string[] text)
    {
        _bannerTitle = title;
        _bannerText = text;
        count = _bannerText.Length;
        float slideTime = 0.25f;
        _width = Raylib.GetScreenWidth();
        _height = height;
        _y = y;
        _xPos = _width;
        var ease = Ease.EaseOutCirc;
        textAnim = new TweenSequence(new IntTween((x) => _xPos = x, _width, 0, slideTime, Ease.EaseInCirc));
        _textDrawXPos = new int[count];
        _textPadXPos = new int[count];
        _fontSizes = new int[count];
        for (int i = 0; i < count; i++)
        {
            _textDrawXPos[i] = _width + 1;
            _fontSizes[i] = height / 4;
        }
        for (int i = 0; i < _bannerText.Length; i++)
        {
            _textDrawXPos[i] = _width;
            var tx = Raylib.MeasureTextEx(Program.headerFont, text[i], _fontSizes[i], 0);
            
            //shrink font until it fits.
            while (tx.X > _width)
            {
                _fontSizes[i] -= 5;
                tx = Raylib.MeasureTextEx(Program.headerFont, text[i], _fontSizes[i], 0);
            }
            _textPadXPos[i] = (int)(_width - tx.X) / 2;

            int j = i;
            var enter = new IntTween((x) => _textDrawXPos[j] = x, _width, 0, slideTime, ease);
            var delay = new IntTween(x =>
            {
                //Console.WriteLine("nop");
            }, 0,0,0.5f, ease);
            if (i > 0)
            {
                int k = i - 1;
                var exit  = new IntTween((x) => _textDrawXPos[k] = x, 0, -_width, slideTime, ease);
                textAnim.Add(new TweenSequence(new TweenGroup(0,enter,exit), delay));
            }
            else
            {
                Debug.Assert(i == 0);
                _textDrawXPos[0] = 0;
                textAnim.Add(new NopTween(slideTime*2));
            }
            //_textExitTween[i] =  new IntTween((x) => _textDrawXPos[j] = x, 0, -_width, 0.5f);
        }

       // var lastExit = new IntTween((x) => _textDrawXPos[count - 1] = x, 0, -_width, slideTime, ease);
        //ExitTween = new TweenGroup(0,  new IntTween((x) => _xPos = x, 0, -_width, slideTime, Ease.EaseInCirc));
        ExitTween = new IntTween((x) => _xPos = x, 0, -_width, slideTime, Ease.EaseInQuad);

        if (includeExitInSequence)
        {
            textAnim.Add(ExitTween);
        }
    }
    
    public void Tick(float delta)
    {
        textAnim.Tick(delta);
    }

    public void Draw()
    {
        Raylib.DrawRectangle(_xPos, _y, _width, _height, Background);
        Raylib.DrawTextEx(Program.headerFont, _bannerTitle, new Vector2(_xPos+6, _y),_height/4, 0, Foreground);

        for (int i = 0; i < count; i++)
        {
            Raylib.DrawTextEx(Program.headerFont, _bannerText[i], new Vector2(_xPos+_textDrawXPos[i]+_textPadXPos[i], _y+_height/4+_fontSizes[i]/2),_fontSizes[i], 0, Foreground);
        }
    }
}