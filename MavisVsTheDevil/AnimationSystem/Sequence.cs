namespace MavisVsTheDevil.Animation;

public class TweenSequence : TweenBase
{
    private readonly TweenBase[] _tweens;

    public TweenSequence(params TweenBase[] tweens) : base(0)
    {
        _tweens = tweens;
        TotalTime = _tweens.Sum(t => t.TotalTime);
    }

    public override void Evaluate(float t)
    {
        float c = _tweens.Length-1;
        int index = (int)MathF.Floor(t * c - Single.Epsilon);
        float remainder = (t * c) % 1;
        _tweens[index].Evaluate(remainder);
    }
}