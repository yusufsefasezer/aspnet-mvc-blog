using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace aspnet_mvc_blog.Areas.Admin.Controllers
{
    public class CategoriesController : ControllerBase
    {
        public ActionResult Index()
        {
            ViewModels.CategoryViewModel model = new ViewModels.CategoryViewModel();
            model.Categories = db.Categories.OrderBy(p => p.Name).ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(aspnet_mvc_blog.Models.Entity.Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (category.Slug == null)
                    {
                        category.Slug = category.Name.GenerateSlug();
                    }
                    category.Slug?.GenerateSlug();

                    db.Categories.Add(category);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "Someting went wrong.");
                }
            }
            return View(new ViewModels.CategoryViewModel { Category = category, Categories = db.Categories.ToList() });
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewModels.CategoryViewModel model = new ViewModels.CategoryViewModel();
            model.Categories = db.Categories.ToList();
            model.Category = db.Categories.Find(id);

            if (model.Categories == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ViewModels.CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(model.Category).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError(string.Empty, "Someting went wrong.");
                }
            }

            return View(new ViewModels.CategoryViewModel { Category = model.Category, Categories = db.Categories.ToList() });
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            aspnet_mvc_blog.Models.Entity.Category category = db.Categories.Find(id);
            if (category == null) return HttpNotFound();
            try
            {
                category.Entries.ToList()
                    .ForEach(p => db.Entry(p).State = System.Data.Entity.EntityState.Deleted);
                db.Categories.RemoveRange(category.SubCategories);
                db.Categories.Remove(category);
                db.SaveChanges();
            }
            catch { }

            return RedirectToAction("Index");
        }
    }
}