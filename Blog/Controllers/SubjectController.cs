using PB.Application;
using PB.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class SubjectController : Controller
    {
        public ActionResult Index()
        {
            var subjects = new SubjectApp().listAll();
            return View(subjects);
        }

        public ActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(Subject subject)
        {
            new SubjectApp().chooseQuery(subject);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int subject_code)
        {
            var subject = new SubjectApp().listByCode(subject_code);
            return View(subject);
        }

        [HttpPost]
        public ActionResult Edit(Subject subject)
        {
            new SubjectApp().chooseQuery(subject);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int subject_code)
        {
            new SubjectApp().deleteSubject(subject_code);
            return RedirectToAction("Index");
        }
    }
}