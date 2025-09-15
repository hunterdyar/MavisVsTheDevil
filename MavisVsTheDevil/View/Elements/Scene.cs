using MavisVsTheDevil.Panels;
using Raylib_cs;

namespace MavisVsTheDevil.Elements;

public class Scene
{
	private bool _withDemon = false;
	private bool _withMavis = false;
	public Scene()
	{
	}

	public void Draw()
	{
		Raylib.BeginMode3D(FightWindow.Camera);
		if(_withDemon)
		{
			AssetManager.Demon.Draw3D(FightWindow.Camera, Raylib.GetFrameTime());
		}

		if (_withMavis)
		{
			AssetManager.Mavis.Draw3D(FightWindow.Camera, Raylib.GetFrameTime());
		}
		
		Raylib.EndMode3D();
	}

	public void SetDemon(bool b)
	{
		_withDemon = b;
	}

	public void SetMavis(bool b)
	{
		_withMavis = b;
	}
}