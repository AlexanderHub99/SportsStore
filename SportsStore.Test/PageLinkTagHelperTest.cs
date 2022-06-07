using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using SportsStore.Infrastructure;
using SportsStore.ViewModels;
using Xunit;

namespace SportsStore.Test
{
    public class PageLinkTagHelperTest
    {
        [Fact]
        public void Can_Ganerate_Page_Links()
        {
            var urlHelper = new Mock<IUrlHelper>();
            urlHelper.SetupSequence(x => x.Action(It.IsAny<UrlActionContext>()))
                .Returns("Test/Page3");

            var urlHelperFactory = new Mock<IUrlHelperFactory>();
            urlHelperFactory.Setup((f => f.GetUrlHelper(It.IsAny<ActionContext>())))
                .Returns(urlHelper.Object);

            PageLinkTagHelper helper = new(urlHelperFactory.Object)
            {
                PageModel = new()
                {
                    CurrentPage = 2,
                    TotalItems = 28,
                    InemsPerPage = 10,
                },
                PagwAction = "Test"
            };

            TagHelperContext ctx = new(
                new(),
                new Dictionary<object, object>(), "");

            var context = new Mock<TagHelperContext>();

            TagHelperOutput output = new("div", new TagHelperAttributeList
                {
                    {
                        "href", "Test/Page3"
                    }
                },
                (cache, encoder) => Task.FromResult<TagHelperContent>(null));


            helper.Process(ctx, output);

            
            var attribute = Assert.Single(output.Attributes);
            Assert.Equal("href", attribute.Name, StringComparer.Ordinal);
            var attributeValue = Assert.IsType<string>(attribute.Value);
            Assert.Equal("Test/Page3", attributeValue, StringComparer.Ordinal);
        }
        
    }
}