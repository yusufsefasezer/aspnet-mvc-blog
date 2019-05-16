using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace aspnet_mvc_blog.Areas.Admin.Controllers
{
    public class PagesController : ControllerBase
    {

        public ActionResult Index()
        {
            UserID = Convert.ToInt32(HttpContext.User.Identity.Name);
            List<Models.Entity.Entry> model = (from Posts in db.Entries
                                               where Posts.Type == Models.Entity.EntryType.Page
                                               where Posts.AuthorID == UserID
                                               orderby Posts.ID descending
                                               select Posts).ToList();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(aspnet_mvc_blog.Models.Entity.Entry entry)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    entry.Type = Models.Entity.EntryType.Page;
                    entry.Slug = entry.Title.GenerateSlug();
                    UserID = Convert.ToInt32(HttpContext.User.Identity.Name);
                    entry.AuthorID = UserID;
                    entry.UpdatedAt = DateTime.Now;
                    if (entry.CreatedAt == null) entry.CreatedAt = DateTime.Now;

                    db.Entries.Add(entry);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError(string.Empty, "Someting went wrong.");
                }
            }

            return View(entry);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Models.Entity.Entry entry = db.Entries.Find(id);
            if (entry == null) return HttpNotFound();
            UserID = Convert.ToInt32(HttpContext.User.Identity.Name);
            if (entry.AuthorID != UserID) return HttpNotFound();

            return View(entry);
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(aspnet_mvc_blog.Models.Entity.Entry entry)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var currentEntry = db.Entries.FirstOrDefault(p => p.ID == entry.ID && p.Type == aspnet_mvc_blog.Models.Entity.EntryType.Page);
                    if (currentEntry == null) return HttpNotFound();

                    currentEntry.Title = entry.Title;
                    currentEntry.Slug = entry.Title.GenerateSlug();
                    currentEntry.Content = entry.Content;
                    currentEntry.Status = entry.Status;
                    currentEntry.CommentStatus = entry.CommentStatus;
                    currentEntry.CreatedAt = entry.CreatedAt;
                    currentEntry.UpdatedAt = DateTime.Now;

                    db.Entry(currentEntry).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError(string.Empty, "Someting went wrong.");
                }
            }

            return View(entry);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Models.Entity.Entry entry = db.Entries.Find(id);
            if (entry == null) return HttpNotFound();
            UserID = Convert.ToInt32(HttpContext.User.Identity.Name);
            if (entry.AuthorID != UserID) return HttpNotFound();

            db.Entries.Remove(entry);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}