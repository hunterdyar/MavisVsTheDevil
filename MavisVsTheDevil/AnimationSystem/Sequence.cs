namespace MavisVsTheDevil.Animation;

public class TweenSequence : TweenBase
{
    private readonly List<TweenBase> _tweens =  new List<TweenBase>();

    public TweenSequence(params TweenBase[] tweens) : base(0)
    {
        _tweens = tweens.ToList();
        TotalTime = _tweens.Sum(t => t.TotalTime);
    }

    public override void Evaluate(float t)
    {
        float c = _tweens.Count-1;
        int index = (int)MathF.Floor(t * c - Single.Epsilon);
        float remainder = (t * c) % 1;
        _tweens[index].Evaluate(remainder);
    }
    public void Add(TweenBase tween)
    {
        _tweens.Add(tween);
        TotalTime = _tweens.Sum(t => t.TotalTime);
    }
}