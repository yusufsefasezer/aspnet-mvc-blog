using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace aspnet_mvc_blog.Areas.Admin.Controllers
{
    public class AuthorsController : ControllerBase
    {
        public ActionResult Index()
        {
            return View(db.Authors.OrderByDescending(p => p.ID).ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(aspnet_mvc_blog.Models.Entity.Author author)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Authors.Add(author);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError(string.Empty, "This E-mail has already been used.");
                }
            }

            return View(author);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            aspnet_mvc_blog.Models.Entity.Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(aspnet_mvc_blog.Models.Entity.Author author)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(author).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError(string.Empty, "This E-mail has already been used.");
                }
            }

            return View(author);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            aspnet_mvc_blog.Models.Entity.Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }

            db.Authors.Remove(author);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}