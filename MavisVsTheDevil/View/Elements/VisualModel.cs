using System.Numerics;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Raylib_cs;
using static Raylib_cs.Raylib;
namespace MavisVsTheDevil.Elements;

public class VisualModel
{
	public string Name;
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
	private int animFrameRate = 30;
	private int frameCount;
	private int _baseScale = 1;
	private FileInfo loadedModel;

	public VisualModel(string modelPath, bool loop = false)
	{
		Load(modelPath);
		this.loop = loop;

	}
	public void Load(string path)
	{
		var fileInfo = new FileInfo(path);
		Name = fileInfo.Name.ToLower();
		AssetManager.LoadModel(fileInfo, out model, out animations);

		animCount = animations.Length;
		animFrame = new int[animCount];
		if (animations.Length == 0)
		{
			frameCount = 0;
		}
		else
		{
			frameCount = animations.Max(x => x.FrameCount);
		}

	}
	/// <summary>
	/// Should be called from within BeginMode3D
	/// </summary>
	public void Draw3D(float delta)
	{
		//update frame
		if (_playing)
		{
			_playback += delta * Raylib.GetFPS() / (GetFPS()/(float)animFrameRate);
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
			_scale*_baseScale,
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

	public void StopAndResetAnim()
	{
		_playing = false;
		_playback = 0;
		_playbackFrame = 0;

		for (int i = 0; i < animCount; i++)
		{
			animFrame[i] = 0;
			UpdateModelAnimation(model, animations[i], 0);
		}

	}

	public void Dispose()
	{
		//unload loaded things if needed.
	}

	public unsafe void SetTexture(Texture2D texture, int map = 0, int matSlot = -1)
	{
		if (matSlot < 0)
		{
			for (int i = 0; i < model.MaterialCount; i++)
			{
				model.Materials[i].Maps->Texture = texture;
			}
		}
		else
		{
			model.Materials[matSlot].Maps->Texture = texture;
		}
	}

	public void SetScale(float scale)
	{
		_scale = new Vector3(scale, scale, scale);
	}

	public void SetRootScale(int scale)
	{
		_baseScale = scale;
		
	}
}