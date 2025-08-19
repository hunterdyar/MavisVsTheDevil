using Raylib_cs;

namespace MavisVsTheDevil.Elements;

public class Scene
{
	public List<Model> Models;
	public List<VisualModel> VisualModels;
	public void Dispose()
	{
		foreach (var model in Models)
		{
			Raylib.UnloadModel(model);
		}
	}

	public void Draw()
	{
		foreach (var model in VisualModels)
		{
			model.Draw();
		}
	}
}