namespace MavisVsTheDevil.Animation;

public class TweenSequence : TweenBase
{
    private readonly List<TweenBase> _tweens;
    private int _current = 0;
    public TweenSequence(params TweenBase[] tweens) : base(0)
    {
        _tweens = tweens.ToList();
        TotalTime = _tweens.Sum(t => t.TotalTime);
    }

    public override void Tick(float t)
    {
        if (!_tweens[_current].IsComplete)
        {
            _tweens[_current].Tick(t);
        }
        else
        {
            if (_current < _tweens.Count-1)
            {
                _current++;
            }
            else
            {
                _finished = true;
            }
        }
    }

    public override void Evaluate(float t)
    {
        t = float.Clamp(t,0,1);
        float c = _tweens.Count;
        int index = (int)MathF.Floor(t * c - Single.Epsilon);
        index = (int)MathF.Min(index, _tweens.Count - 1);
        float remainder = (t * c) % 1;
        _tweens[index].Evaluate(remainder);
    }

    public override void Reset()
    {
        base.Reset();
        _current = 0;
    }

    public override void Interpolate(float t)
    {
        t = float.Clamp(t, 0, 1);
        float c = _tweens.Count;
        int index = (int)MathF.Floor(t * c - Single.Epsilon);
        index = (int)MathF.Min(index, _tweens.Count - 1);
        float remainder = (t * c) % 1;
        _tweens[index].Interpolate(remainder);
    }

    public void Add(TweenBase tween)
    {
        _tweens.Add(tween);
        TotalTime = _tweens.Sum(t => t.TotalTime);
    }
}