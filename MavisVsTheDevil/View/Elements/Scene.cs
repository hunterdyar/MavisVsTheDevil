using Raylib_cs;

namespace MavisVsTheDevil.Elements;

public class Scene
{
	public readonly List<VisualModel> VisualModels =  new List<VisualModel>();
	private Camera3D camera;

	public Scene(Camera3D camera)
	{
		this.camera = camera;
	}
	public void Dispose()
	{
		foreach (var model in VisualModels)
		{
			model.Dispose();
		}
	}

	public VisualModel? GetModel(int index)
	{
		if (index < 0 || index >= VisualModels.Count)
		{
			return null;
		}
		return VisualModels[index];
	}

	public VisualModel? GetModel(string name)
	{
		return VisualModels.Find(x => x.Name == name);
	}

	public void Draw()
	{
		Raylib.BeginMode3D(camera);
		foreach (var model in VisualModels)
		{
			model.Draw3D(Raylib.GetFrameTime());
		}
		Raylib.EndMode3D();
	}

	public void CreateDemonScene()
	{
		VisualModels.Clear();
		var demon = new VisualModel("Resources/models/scenetest/planepop.glb", true);
		//set texture from round CurrentDemon TexturePath.
		//set position and rotation
		VisualModels.Add(demon);
	}
}