namespace MavisVsTheDevil.Panels;

public abstract class AnimationPanel : PanelBase
{
    public Animation.Animation Primary;

    protected AnimationPanel(GameWindow window) : base(window)
    {
    }

    public virtual void Tick(float delta)
    {
        Primary.Tick(delta);
    }
}