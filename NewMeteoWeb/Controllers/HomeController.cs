using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewMeteoWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Pages/Index.cshtml");
        }

        public IActionResult UserList()
        {
            return View();
        }
    }
}
