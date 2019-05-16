using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace aspnet_mvc_blog.Controllers
{
    public class ErrorController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}