using MavisVsTheDevil.Panels;
using Raylib_cs;

namespace MavisVsTheDevil.Elements;

public class Scene
{
	private bool _withDemon = false;
	private Camera3D camera;

	public Scene()
	{
		this.camera = FightWindow.camera;
	}

	public void Draw()
	{
		Raylib.BeginMode3D(camera);
		if(_withDemon)
		{
			AssetManager.Demon.Draw3D(Raylib.GetFrameTime());
		}
		Raylib.EndMode3D();
	}

	public void SetDemon(bool b)
	{
		_withDemon = b;
	}
}