using Microsoft.AspNet.Razor.TagHelpers;

namespace RecipeBook.TagHelpers
{
    [HtmlTargetElement("p", Attributes = "markdown")]
    public class MarkDownTagHelper : TagHelper
    {
        public void Process(TagHelperContent context, TagHelperOutput output)
        {
            string content = context.GetContent();            
            var markDownParser = new MarkdownSharp.Markdown();
            string markDownHtml = markDownParser.Transform(content);
            
            output.Content.SetHtmlContent(markDownHtml);
        }
    }    
}