using ColorCode.Styling;
using Markdig;
using Markdown.ColorCode;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using SqlInteractive.UI.Services;

namespace SqlInteractive.UI.DynamicPages;

/// <summary>
/// A Component for displaying Markdown.
/// </summary>
public class Markdown : ComponentBase
{
	/// <summary>
	/// Gets or sets the path to the Markdown file.
	/// </summary>
	[Parameter]
	public string FilePath { get; set; }

	[Inject]
	public IStaticFilesService staticFiles { get; set; }

	private MarkupString _markupString = new();

	/// <summary>
	/// Gets the <see cref="MarkdownPipeline"/> to use.
	/// </summary>
	public virtual MarkdownPipeline Pipeline => new MarkdownPipelineBuilder()
		.UseEmojiAndSmiley()
		.UseAdvancedExtensions()
		.UseColorCode(StyleDictionary.DefaultLight)
		.Build();

	/// <inheritdoc/>
	protected override void BuildRenderTree(RenderTreeBuilder builder)
	{
		base.BuildRenderTree(builder);
		builder.AddContent(0, _markupString);
	}

	protected override async Task OnParametersSetAsync()
	{
		await base.OnParametersSetAsync();
		var markdown = await staticFiles.GetTextFromFile(FilePath);
		_markupString = new MarkupString(Markdig.Markdown.ToHtml(markdown, Pipeline));
	}
}