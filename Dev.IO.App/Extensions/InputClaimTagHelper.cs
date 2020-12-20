using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev.IO.App.Extensions
{
    [HtmlTargetElement("*", Attributes = "supress-by-claim-name")]
    [HtmlTargetElement("*", Attributes = "supress-by-claim-value")]
    public class SupressOutputClaimTagHelper : TagHelper
    {
        //Acessa o Contexto via HTTP
        private readonly IHttpContextAccessor _contextAccessor;
        public SupressOutputClaimTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }


        [HtmlAttributeName("supress-by-claim-name")]
        public string IdentityClaimName { get; set; }


        [HtmlAttributeName("supress-by-claim-value")]
        public string IdentityClaimValue { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            bool hasAcess = CustomAuthorize.ValidateClaimsUser(context: _contextAccessor.HttpContext, IdentityClaimName, IdentityClaimValue);

            if (hasAcess) return;

            output.SuppressOutput();

        }
    }

    [HtmlTargetElement("a", Attributes = "disable-by-claim-name")]
    [HtmlTargetElement("a", Attributes = "disable-by-claim-value")]
    public class DisableElementClaimTagHelper : TagHelper
    {
        //Acessa o Contexto via HTTP
        private readonly IHttpContextAccessor _contextAccessor;

        public DisableElementClaimTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HtmlAttributeName("disable-by-claim-name")]
        public string IdentityClaimName { get; set; }

        [HtmlAttributeName("disable-by-claim-value")]
        public string IdentityClaimValue { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            bool hasAcess = CustomAuthorize.ValidateClaimsUser(context: _contextAccessor.HttpContext, IdentityClaimName, IdentityClaimValue);

            if (hasAcess) return;

            output.Attributes.RemoveAll("href");
            output.Attributes.Add(new TagHelperAttribute(name: "style", value: "cursor: not-allowed"));
            output.Attributes.Add(new TagHelperAttribute(name: "title", value: "Você não tem Permissão!"));

        }
    }


    [HtmlTargetElement("*", Attributes = "supress-by-action")]
    public class DisableLinkClaimTagHelper : TagHelper
    {
        //Acessa o Contexto via HTTP
        private readonly IHttpContextAccessor _contextAccessor;
        public DisableLinkClaimTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HtmlAttributeName("supress-by-action")]
        public string ActionName { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            string action = _contextAccessor.HttpContext.GetRouteData().Values["action"].ToString();

            if (ActionName.Contains(action)) return;

            output.SuppressOutput();

        }
    }
}
