using MavisVsTheDevil.Animation;

namespace MavisVsTheDevil.Panels;

public abstract class AnimationPanel : PanelBase
{
    public TweenBase Primary;

    protected AnimationPanel(GameWindow window) : base(window)
    {
        
    }

    public virtual void Start()
    {
        Primary.Reset();
    }
    public virtual void Tick(float delta)
    {
        Primary.Tick(delta);
    }
}