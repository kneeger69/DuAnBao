using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DuAnBao.Models;
using EntityState = System.Data.Entity.EntityState;

namespace DuAnBao.Controllers
{
    public class ArticlesController : Controller
    {
        private ArticleManagementDBEntities1 db = new ArticleManagementDBEntities1();

        // GET: Articles
        public ActionResult Index()
        {
            var articles = db.Articles.Include(a => a.User_reader).ToList();

            ViewBag.MagazinesCount = 28;  
            ViewBag.ArticlesCount = articles.Count;
            ViewBag.VisitCount = 115499; 
            ViewBag.DownloadCount = 33518;
            var latestArticles = db.Articles
                               .OrderByDescending(a => a.PublishedDate)
                               .Take(6)
                               .ToList();
            ViewBag.LatestArticles = latestArticles;

            return View(articles);
        }

        public ActionResult UserInfo()
        {
            return View();
        }

        public ActionResult Introduce()
        {
            return View();
        }

        // GET: Articles/Notification
        public ActionResult Notification()
        {
            var notifications = db.Articles.ToList();
            return View(notifications);
        }

        // GET: Articles/Send
        public ActionResult Send()
        {
            return View();
        }

        // GET: Articles/SignIn
        public ActionResult SignIn()
        {
            return View();
        }

       /* // POST: Articles/SignIn
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Add authentication logic here
                return RedirectToAction("Index"); // Redirect to appropriate action
            }
            return View(model);
        }*/

        // GET: Articles/SignUp
        public ActionResult SignUp()
        {
            return View();
        }

       /* // POST: Articles/SignUp
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Add user registration logic here
                return RedirectToAction("Index"); // Redirect to appropriate action
            }
            return View(model);
        }
*/

        public ActionResult Approve()
        {
            var approves = db.Articles.OrderByDescending(a => a.PublishedDate).ToList();
            return View(approves);
        }

        public ActionResult Pending()
        {
            var pending = db.Articles.ToList();
            return View(pending);
        }

        // GET: Articles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Articles/Create
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.User_reader, "UserId", "Username");
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArticleId,Title,Summary,Content,PublishedDate,AuthorId")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(db.User_reader, "UserId", "Username", article.AuthorId);
            return View(article);
        }

        // GET: Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.User_reader, "UserId", "Username", article.AuthorId);
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArticleId,Title,Summary,Content,PublishedDate,AuthorId")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.User_reader, "UserId", "Username", article.AuthorId);
            return View(article);
        }

        // GET: Articles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
