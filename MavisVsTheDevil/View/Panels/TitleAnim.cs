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
        _suffixAnim = new IndexTween<string>((v) => { text = prefix + v; _color = Color.White;}, suffix, 2);
        _fadeOutAnim = new ColorTween((c) =>
        {
            _color = c;
        }, Color.White, new Color(255,255,255,0), 0.25f);
        _sequence = new TweenSequence(_suffixAnim, _fadeOutAnim, new NopTween(0.5f));
        Primary = _sequence;
    }
    
    public override void Draw()
    {
        DrawUtility.DrawLineCentered(text, Width, PosY+Height/2, 32,_color,0);
    }
}
