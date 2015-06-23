using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using QueryTampering.Utils;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace QueryTampering.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/{id}
        public IActionResult Index()
        {
            ViewBag.Url = string.Concat("/Home/show?", Security.CreateTamperProofQueryString("id=3"));
            return View();
        }

        public IActionResult Show(int id)
        {
            return View(id);
        }
    }
}
