using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev.IO.App.Extensions
{
    public static class RazorExtensions
    {

        public static string FormatDocument(this RazorPage page, int supplierType, string document)
        {
            return supplierType == 1 ? Convert.ToUInt64(document).ToString(format: @"000\.000\.000\-00") : Convert.ToUInt64(document).ToString(format: @"00\.000\.000\/0000\-00");
        }

    }
}
