using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace aspnet_mvc_blog.Areas.Admin.Controllers
{
    [AllowAnonymous]
    public class LogoutController : ControllerBase
    {
        public ActionResult Index()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}