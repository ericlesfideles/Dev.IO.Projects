using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AppMvcBasic.Models;
using Dev.IO.App.Extensions;

namespace Dev.IO.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("error/{id:length(3,3)}")]
        public IActionResult Erros(int id)
        {
            CustomErroDetail erroDetail = new CustomErroDetail();
            var e = erroDetail.GetErroDetailByid(id);
            
            return View("Error", e);
        }

    }
}
