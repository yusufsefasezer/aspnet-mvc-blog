using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace aspnet_mvc_blog.Areas.Admin.Controllers
{
    public class PostsController : ControllerBase
    {

        public ActionResult Index()
        {
            UserID = Convert.ToInt32(HttpContext.User.Identity.Name);
            List<Models.Entity.Entry> model = (from Posts in db.Entries
                                               where Posts.Type == Models.Entity.EntryType.Post
                                               where Posts.AuthorID == UserID
                                               orderby Posts.ID descending
                                               select Posts).ToList();

            return View(model);
        }
        public ActionResult Create()
        {
            ViewModels.PostViewModel model = new ViewModels.PostViewModel();
            model.Categories = db.Categories.ToList();
            return View(model);
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModels.PostViewModel model)
        {
            model.Categories = db.Categories.ToList();

            if (ModelState.IsValid)
            {
                try
                {
                    model.Entry.Type = Models.Entity.EntryType.Post;
                    model.Entry.Slug = model.Entry.Title.GenerateSlug();
                    UserID = Convert.ToInt32(HttpContext.User.Identity.Name);
                    model.Entry.AuthorID = UserID;
                    model.Entry.UpdatedAt = DateTime.Now;
                    model.Entry.Categories = db.Categories.Where(p => model.SelectedCategories.Contains(p.ID)).ToList();
                    if (model.Entry.CreatedAt == null) model.Entry.CreatedAt = DateTime.Now;

                    db.Entries.Add(model.Entry);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError(string.Empty, "Someting went wrong.");
                }
            }

            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ViewModels.PostViewModel model = new ViewModels.PostViewModel();
            model.Entry = db.Entries.Find(id);

            if (model.Entry == null) return HttpNotFound();
            UserID = Convert.ToInt32(HttpContext.User.Identity.Name);
            if (model.Entry.AuthorID != UserID) return HttpNotFound();

            model.Categories = db.Categories.ToList();
            model.SelectedCategories = model.Entry.Categories.Select(p => p.ID).ToList();

            return View(model);
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ViewModels.PostViewModel model)
        {
            model.Categories = db.Categories.ToList();

            if (ModelState.IsValid)
            {
                try
                {

                    var currentEntry = db.Entries.FirstOrDefault(p => p.ID == model.Entry.ID && p.Type == aspnet_mvc_blog.Models.Entity.EntryType.Post);
                    if (currentEntry == null) return HttpNotFound();

                    currentEntry.Title = model.Entry.Title;
                    currentEntry.Slug = model.Entry.Title.GenerateSlug();
                    currentEntry.Content = model.Entry.Content;
                    currentEntry.Status = model.Entry.Status;
                    currentEntry.CommentStatus = model.Entry.CommentStatus;
                    currentEntry.CreatedAt = model.Entry.CreatedAt;
                    currentEntry.UpdatedAt = DateTime.Now;
                    currentEntry.Categories
                        .ToList()
                        .ForEach(p => currentEntry.Categories.Remove(p));
                    currentEntry.Categories = db.Categories
                        .Where(p => model.SelectedCategories.Contains(p.ID))
                        .ToList();

                    db.Entry(currentEntry).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError(string.Empty, "Someting went wrong.");
                }
            }

            return View(model);
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