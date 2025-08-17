namespace MavisVsTheDevil.Panels;

public abstract class PanelBase
{
	public int PosX;
	public int PosY;
	public int Width;
	public int Height;

	public PanelBase()
	{
		
	}

	public void Resize(int posX, int posY, int width, int height)
	{
		PosX = posX;
		PosY = posY;
		Width = width;
		Height = height;
		OnResize();
	}

	protected virtual void OnResize()
	{
		
	}

	public abstract void Draw();

	public virtual void OnClose()
	{
		
	}
}