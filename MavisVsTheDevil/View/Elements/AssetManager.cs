using MavisVsTheDevil.Demons;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Raylib_cs;

namespace MavisVsTheDevil.Elements;

public static class AssetManager
{
	private static readonly Dictionary<FileInfo, Model> _models = new Dictionary<FileInfo, Model>();
	private static readonly Dictionary<FileInfo, ModelAnimation[]> _modelAnimations = new Dictionary<FileInfo, ModelAnimation[]>();
	private static readonly Dictionary<FileInfo, Texture2D> _textures = new Dictionary<FileInfo, Texture2D>();

	public static VisualModel Demon;
	public const string texturePath = "Resources/demons/";
	
	public static void Initiate()
	{
		Demon = new VisualModel("Resources/models/demon.glb");
		Demon.SetScale(3);
		Demons.Demon.OnDemonChosen += SetDemonTexture;
	}

	public static void SetDemonTexture(Demon demon)
	{
		string filename = demon.imagePath;
		if (string.IsNullOrEmpty(filename))
		{
			return;
		}
		var path = texturePath + filename;
		var info = new FileInfo(path);
		LoadTexture(info, out var texture);
		Demon.SetTexture(texture);
	}
	
	public static void LoadTexture(FileInfo file, out Texture2D texture)
	{
		if (_textures.ContainsKey(file))
		{
			texture = _textures[file];
			return;
		}

		texture = Raylib.LoadTexture(file.FullName);
		_textures.Add(file,texture);
	}
	public static unsafe void LoadModel(FileInfo modelFile, out Model model, out ModelAnimation[] animations)
	{
		if (!modelFile.Exists)
		{
			throw new FileNotFoundException(modelFile.Name);
		}
		
		//don't load if already loaded.
		if(_models.ContainsKey(modelFile))
		{
			model = _models[modelFile];
			animations = [];
			if (_modelAnimations.ContainsKey(modelFile))
			{
				animations = _modelAnimations[modelFile];
			}

			return;
		}

		model = Raylib.LoadModel(modelFile.FullName);
		_models.Add(modelFile, model);

		int count = 0;
		var anims = Raylib.LoadModelAnimations(modelFile.FullName, ref count);
		animations = new ModelAnimation[count];
		for (int i = 0; i < count; i++)
		{
			animations[i] = anims[i];
		}

		_modelAnimations.Add(modelFile, animations);

		Console.WriteLine($"Loaded {modelFile.Name}. Animation Count: {count}");
	}

	public static unsafe void UnloadAll()
	{
		var allModels = _models.Values.ToArray();
		var alltex = _textures.Values.ToArray();
		var allAnims = _modelAnimations.Values.ToArray();
		foreach (var model in allModels)
		{
			Raylib.UnloadModel(model);
		}


		foreach (var tex in alltex)
		{
			Raylib.UnloadTexture(tex);
		}
	}
}