using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;


[Generator(LanguageNames.CSharp)]
public class WordlistGenerator : Microsoft.CodeAnalysis.IIncrementalGenerator
{

	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		IncrementalValuesProvider<AdditionalText> textFiles = context.AdditionalTextsProvider.Where( file => file.Path.EndsWith(".txt"));

		// read their contents and save their name
		var sourceBuilder = new StringBuilder();
		
		
		IncrementalValuesProvider<(string name, string content)> namesAndContents =
			textFiles.Select((text, cancellationToken) => (name: Path.GetFileNameWithoutExtension(text.Path),
				content: text.GetText(cancellationToken).ToString()));
		
		sourceBuilder.Append("}");
		context.RegisterSourceOutput(namesAndContents, (spc, nameAndContent) =>
		{
			sourceBuilder.Append(@"namespace Wordlist;
public static partial class Wordlist 
{");
			GenerateFromList(sourceBuilder, nameAndContent.name, nameAndContent.content);
			sourceBuilder.Append("}");

			spc.AddSource($"WordLists.{nameAndContent.name}.cs", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
		});
	}
	
	public void GenerateFromList(StringBuilder sb, string name, string source)
	{
		if (string.IsNullOrEmpty(source))
		{
			return;
		}
		sb.Append($"""
		          public static readonly string[] {name} = [
		          """);
		var split = source.Split('\n');
		for (var i = 0; i < split.Length; i++)
		{
			string line = split[i].ToString();
			if (!string.IsNullOrEmpty(line))
			{
				//replace quote with slash quote. fuck this was annoying to type.
				sb.Append("\"");
				sb.Append(line.Replace("\"", "\\\""));
				sb.Append("\"");
				if (i < split.Length - 1)
				{
					sb.Append(", ");
				}
			}
		}

		sb.Append(@"];
");
	}


}