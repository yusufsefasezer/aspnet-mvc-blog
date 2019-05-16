using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using aspnet_mvc_blog.Models.Entity;

namespace aspnet_mvc_blog.Areas.Admin.Controllers
{
    [AllowAnonymous]
    public class RegisterController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Author author)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Authors.Add(author);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Login");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(new ViewModels.RegisterViewModel());
        }
    }
}