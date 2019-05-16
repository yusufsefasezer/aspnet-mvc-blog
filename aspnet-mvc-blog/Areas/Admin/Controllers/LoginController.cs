using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using aspnet_mvc_blog.Models.Entity;

namespace aspnet_mvc_blog.Areas.Admin.Controllers
{
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ViewModels.LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (db.Authors.Any(p => p.Email == model.Email && p.Password == model.Password))
                    {
                        var userID = db.Authors.FirstOrDefault(p => p.Email == model.Email && p.Password == model.Password).ID;
                        FormsAuthentication.SetAuthCookie(userID.ToString(), false);
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError(string.Empty, "E-mail or password is incorrect.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(new ViewModels.LoginViewModel());
        }
    }
}