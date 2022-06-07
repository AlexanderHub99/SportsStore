using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SportsStore.ViewModels;

namespace SportsStore.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-models")]
    public class PageLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory _helperFactory;

        public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _helperFactory = urlHelperFactory;
        } 
        
        [ViewContext]
        [HtmlAttributeNotBound]
        public  ViewContext ViewContext { get; set; }
        public PagingInfo PageModel { get; set; }
        public string PagwAction { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = _helperFactory.GetUrlHelper(ViewContext);
            TagBuilder result = new TagBuilder("div");
            for (int i = 1; i <-PageModel.TotalItems; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.Attributes["href"] = urlHelper.Action(PagwAction, new {page = i});
                tag.InnerHtml.Append(i.ToString());
                result.InnerHtml.AppendHtml(tag);
            }

            output.Content.AppendHtml(result.InnerHtml);
        }
        
    }
}

