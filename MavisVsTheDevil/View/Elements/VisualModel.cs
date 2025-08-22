using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
namespace MavisVsTheDevil.Elements;

public class VisualModel
{
	private Vector3 position;
	private Vector3 rotationAxis = new Vector3(0, 1, 0);
	private float rotationAngle = 0;
	private Vector3 _scale = Vector3.One;
	private Color _tint = Color.White;

	private bool loop;

	private Model model;
	private int animCount;
	private ModelAnimation[] animations;
	private int[] animFrame;
	private int _playbackFrame;
	private bool _playing;
	private float _playback;

	private int frameCount;

	private FileInfo loadedModel;

	public VisualModel(string modelPath)
	{
		Load(modelPath);
		
	}
	public void Load(string path)
	{
		var fileInfo = new FileInfo(path);
		AssetManager.LoadModel(fileInfo, out model, out animations);
		animCount = animations.Length;
		animFrame = new int[animCount];
		frameCount = animations.Max(x => x.FrameCount);
	}
	/// <summary>
	/// Should be called from within BeginMode3D
	/// </summary>
	public void Draw3D(float delta)
	{
		//update frame
		if (_playing)
		{
			_playback += delta * Raylib.GetFPS();
			_playbackFrame = (int)MathF.Floor(_playback);

			if (_playbackFrame >= frameCount)
			{
				if (loop)
				{
					_playback = 0;
					_playbackFrame = 0;
				}
				else
				{
					_playing = false;
					//wait go back to the last frame so it stops there? bleh
					_playbackFrame = frameCount - 1;
				}
			}
			
			for (int i = 0; i < animCount; i++)
			{
				animFrame[i] = _playbackFrame % animations[i].FrameCount;
				UpdateModelAnimation(model, animations[i], animFrame[i]);
			}
		}

		DrawModelEx(
			model,
			position,
			rotationAxis,
			rotationAngle,
			_scale,
			_tint
		);
	}

	public void Play(bool resume = true)
	{
		_playing = true;
		if (!resume)
		{
			_playbackFrame = 0;
		}
	}
	public void Pause()
	{
		_playing = false;
	}

	public void Stop()
	{
		_playing = false;
		_playback = 0;
	}

	public void Dispose()
	{
		//unload loaded things if needed.
	}
}