﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dev.IO.App.Extensions
{
    public class CustomAuthorize
    {
        public static   bool ValidateClaimsUser(HttpContext context, string claimName, string claimValue)
        {
            return context.User.Identity.IsAuthenticated &&
                    context.User.Claims.Any(e => e.Type == claimName && e.Value.Contains(claimValue));
        }


    }

    public class ClaimsAuthorizeAttribute: TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(RequireClaimFilter))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }

    public class RequireClaimFilter : IAuthorizationFilter
    {
        private readonly Claim _claim;

        public RequireClaimFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(values: new { area = "Identity", page = "/account/Login", ReturnUrl = context.HttpContext.Request.Path.ToString() }));
            }

            if (!CustomAuthorize.ValidateClaimsUser(context.HttpContext, _claim.Type, _claim.Value))
            {
                context.Result = new StatusCodeResult(statusCode: 403);
            }
        }
    }
        

}
