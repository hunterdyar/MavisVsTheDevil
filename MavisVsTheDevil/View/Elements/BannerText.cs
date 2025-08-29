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
    private int _height;
    private int _width;
    private int _y;
    private int _currentTextEntering = -1;
    
    //todo: xPadding to center the text.
    public BannerText(int height,int y, string title, params string[] text)
    {
        _bannerTitle = title;
        _bannerText = text;
        count = _bannerText.Length;
        
        _width = Raylib.GetScreenWidth();
        _height = height;
        _y = y;
        textAnim = new TweenSequence(new IntTween((x) => _xPos = x, _width, 0, 0.5f));
        _textDrawXPos = new int[count];
        
        for (int i = 0; i < count; i++)
        {
            _textDrawXPos[i] = _width + 1;
        }
        for (int i = 0; i < _bannerText.Length; i++)
        {
            _textDrawXPos[i] = _width;
            int j = i;
            var enter = new IntTween((x) => _textDrawXPos[j] = x, _width, 0, 0.5f);
            var delay = new IntTween(x =>
            {
                Console.WriteLine("nop");
            }, 0,0,0.5f);
            if (i > 0)
            {
                int k = i - 1;
                var exit  = new IntTween((x) => _textDrawXPos[k] = x, 0, -_width, 0.5f);
                textAnim.Add(new TweenSequence(new TweenGroup(0,enter,exit), delay,delay));
            }
            else
            {
                textAnim.Add(new TweenSequence(enter, delay,delay));
            }
            //_textExitTween[i] =  new IntTween((x) => _textDrawXPos[j] = x, 0, -_width, 0.5f);
        }
        var lastExit  = new IntTween((x) => _textDrawXPos[count-1] = x, 0, -_width, 0.5f);
        textAnim.Add(lastExit);
        textAnim.Add(new IntTween((x) => _xPos = x, _xPos, -_width, 0.5f));

    }
    
    public void Tick(float delta)
    {
        textAnim.Tick(delta);
    }

    public void Draw()
    {
        Raylib.DrawRectangle(_xPos, _y, _width, _height, Color.Black);
        Raylib.DrawTextEx(Program.terminalFont, _bannerTitle, new Vector2(_xPos, _y),_height/4, 0, Color.White);

        for (int i = 0; i < count; i++)
        {
            Raylib.DrawTextEx(Program.terminalFont, _bannerText[i], new Vector2(_textDrawXPos[i], _y),_height/2, 0, Color.White);
        }
        // //Text entering.
        // if (_currentTextEntering >= 0)
        // {
        //     Raylib.DrawTextEx(Program.terminalFont, _bannerText[_currentTextEntering], new Vector2(_textDrawXPos[_currentTextEntering], _y),_height/2, 0, Color.White);
        // }
        //
        // if (_currentTextEntering > 0)
        // {
        //     int exit = _currentTextEntering - 1;
        //     Raylib.DrawTextEx(Program.terminalFont, _bannerText[exit], new Vector2(_textDrawXPos[exit], _y),_height/2, 0, Color.White);
        // }
        //
    }
}