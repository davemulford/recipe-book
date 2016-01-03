using System.Threading.Tasks;
using MarkdownSharp;
using Microsoft.AspNet.Razor.TagHelpers;

namespace RecipeBook.TagHelpers
{
    [HtmlTargetElement("p", Attributes = "markdown")]
    public class MarkDownTagHelper : TagHelper
    {
        private static readonly Markdown MarkdownParser = new Markdown();
        
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await output.GetChildContentAsync();            
            string transformedContent = MarkdownParser.Transform(childContent.GetContent().Replace("&#xD;&#xA;", "\r\n"));
            
            output.Content.SetHtmlContent(transformedContent);
        }
    }    
}