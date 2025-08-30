namespace MavisVsTheDevil.Animation;

public class TweenGroup : TweenBase
{
    public List<TweenBase> Tweens = new List<TweenBase>();
    
    public TweenGroup(float total) : base(total)
    {
        Tweens = new List<TweenBase>();
    }

    public TweenGroup(float total, params TweenBase[] tweens) :  base(total)
    {
        foreach (var tween in tweens)
        {
            Tweens.Add(tween);
        }
        TotalTime = Tweens.Max(x=>x.TotalTime);
    }

    public void AddTween(TweenBase tween)
    {
        Tweens.Add(tween);
        TotalTime = Tweens.Max(x=>x.TotalTime);
    }

    public override void Evaluate(float t)
    {
        foreach (var tween in Tweens)
        {
            tween.Evaluate(t);
        }
    }

    public override void Interpolate(float t)
    {
        foreach (var tween in Tweens)
        {
            tween.Interpolate(t);
        }
    }
}