using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SportsStore.ViewModels;

namespace SportsStore.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory _helperFactory;

        public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _helperFactory = urlHelperFactory;
        } 
        
        [ViewContext]
        [HtmlAttributeNotBound]
        public  ViewContext ViewContext { get; set; } = null!;

        public string CurrentPage { get; set; } = null!;
        public string TotalPages { get; set; } = null!;
        public string PageAction { get; set; } = null!;

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();

        public bool  PageClassesEnabled { get; set; }
        public string PageClassSelected { get; set; } = null!;
        public string PageClassNormal { get; set; } = null!;
        public string PageClass { get; set; } = null!;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = _helperFactory.GetUrlHelper(ViewContext);
            TagBuilder result = new TagBuilder("div");
            for (int i = 1; i <= Convert.ToInt32(TotalPages); i++)
            {
                TagBuilder tag = new TagBuilder("a");
                PageUrlValues["page"] = i;
                tag.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
                if (PageClassesEnabled)
                {
                    tag.AddCssClass(PageClass);
                    tag.AddCssClass(i == Convert.ToInt32(CurrentPage) ? PageClassSelected:PageClassNormal);
                }
                tag.InnerHtml.Append(i.ToString());
                result.InnerHtml.AppendHtml(tag);
            }
            output.Content.AppendHtml(result.InnerHtml);
        }
        
    }
}

