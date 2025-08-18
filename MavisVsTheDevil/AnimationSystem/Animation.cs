namespace MavisVsTheDevil.Animation;

public class Animation
{
    public TweenBase _tween;
    private float time = 0;
    private float totalTime;
    
    public bool AtStart => time == 0;
    public bool AtEnd => time >= totalTime;
    public Action OnComplete;
    
    /// <summary>
    /// An animation wraps a tween (evaluated at any time) with callback events and tick (start to end, finish)
    /// </summary>
    public Animation(TweenBase tween, float time)
    {
        _tween = tween;
        totalTime = time;
    }

    public void Tick(float delta)
    {
        time += delta;
        _tween.Evaluate(Math.Clamp(time/totalTime, 0, 1));
        if (time >= totalTime)
        {
            OnComplete?.Invoke();
        }
    }

    public void Reset()
    {
        time = 0;
    }
}