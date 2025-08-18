using System.Numerics;
using MavisVsTheDevil.Animation;
using MavisVsTheDevil.Panels;
using Raylib_cs;

namespace MavisVsTheDevil.GameAnimations;

public class TitleAnim : AnimationPanel
{
    public TweenSequence _sequence;
    private string prefix = "You Have Decided ";

    private string[] suffix =
    [
        "To Suffer",
        "To Reist",
        "To Type",
        "To Struggle",
        "To Fight",
        "To Type",
        "To Despair",
        "To Type",
        "To Struggle",
        "To Type",
        "Wrong",
    ];

    private IndexTween<string> _suffixAnim;
    private ColorTween _fadeOutAnim;
    private string text;
    private Color _color = Color.White;
    public TitleAnim(GameWindow window) : base(window)
    {
        _suffixAnim = new IndexTween<string>((v) => { text = prefix + v; _color = Color.White;}, suffix);
        _fadeOutAnim = new ColorTween((c) =>
        {
            _color = c;
        }, Color.White, Color.Blank);
        _sequence = new TweenSequence(_suffixAnim, _fadeOutAnim, new NopTween());
        Primary = new Animation.Animation(_sequence, 4);
    }
    
    public override void Draw()
    {
        Raylib.DrawTextEx(Program.terminalFont, text, new Vector2(PosX+Width/2 -350,PosY+Height/2),32,0,_color);
        
    }
}
