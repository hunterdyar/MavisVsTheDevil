using Raylib_cs;

namespace MavisVsTheDevil.Elements;

public class Scene
{
	public List<VisualModel> VisualModels;
	private Camera3D camera;

	public void Init()
	{
		
	}
	public void Dispose()
	{
		foreach (var model in VisualModels)
		{
			model.Dispose();
		}
	}

	public void Draw()
	{
		Raylib.BeginMode3D(camera);
		foreach (var model in VisualModels)
		{
			model.Draw3D(Raylib.GetFrameTime());
		}
	}
}