using DevIO.Bussiness.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev.IO.App.Extensions
{
    public class SummaryViewComponent: ViewComponent
    {
        private readonly INotify _notify;
        public SummaryViewComponent(INotify notify)
        {
            _notify = notify;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notifications = await Task.FromResult(_notify.GetNotifications());

            notifications.ForEach(e => ViewData.ModelState.AddModelError(string.Empty, e.Message));

            return View();
        }

    }
}
