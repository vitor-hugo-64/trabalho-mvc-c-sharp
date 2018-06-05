using PB.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Informations()
        {
            var informations = new PostingApp().informations();
            ViewBag.views = informations[0];
            ViewBag.postings = informations[1];
            return View();
        }
    }
}