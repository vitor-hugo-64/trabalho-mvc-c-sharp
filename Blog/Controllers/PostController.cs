using PB.Application;
using PB.Domain;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index()
        {
            var allPostings = new PostingApp().listAll();
            return View(allPostings);
        }

        public ActionResult ViewPosting(int posting_code)
        {
            return View(new PostingApp().visualization(posting_code));
        }

        public ActionResult Insert()
        {
            ViewBag.Subjects = new SubjectApp().listAll();
            return View();
        }

        [HttpPost]
        public ActionResult Insert(Posting posting)
        {
            new PostingApp().chooseQuery(posting);
            return RedirectToAction("Index");
        }

        public ActionResult Update(int posting_code)
        {
            ViewBag.Subjects = new SubjectApp().listAll();
            Posting posting = new PostingApp().listByCode(posting_code);
            return View(posting);
        }

        [HttpPost]
        public ActionResult Update(Posting posting)
        {
            new PostingApp().chooseQuery(posting);
            return RedirectToAction("ViewPosting", new { posting_code = posting.posting_code});
        }

        public ActionResult Delete(int posting_code)
        {
            new PostingApp().deletePosting(posting_code);
            return RedirectToAction("Index");
        }
    }
}