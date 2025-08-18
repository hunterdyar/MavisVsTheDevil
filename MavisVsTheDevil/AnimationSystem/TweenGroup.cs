namespace MavisVsTheDevil.Animation;

public class TweenGroup
{
    public List<TweenBase> Tweens = new List<TweenBase>();

    public TweenGroup()
    {
        Tweens = new List<TweenBase>();
    }

    public TweenGroup(params TweenBase[] tweens)
    {
        foreach (var tween in tweens)
        {
            Tweens.Add(tween);
        }
    }

    public void AddTween(TweenBase tween)
    {
        Tweens.Add(tween);
    }

    public void Evaluate(float t)
    {
        foreach (var tween in Tweens)
        {
            tween.Evaluate(t);
        }
    }
}