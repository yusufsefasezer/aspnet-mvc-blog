using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace aspnet_mvc_blog.Areas.Admin.Controllers
{
    public class OptionsController : ControllerBase
    {
        public ActionResult Index()
        {
            ViewModels.OptionViewModel model = new ViewModels.OptionViewModel();
            string blogStyle = ViewData["BlogStyle"] == null ? "" : ViewData["BlogStyle"].ToString();
            model.Categories = GetStyleList(blogStyle);
            model.Options = db.Options.ToDictionary(p => p.Name, p => p.Value);
            return View(model);
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ViewModels.OptionViewModel model)
        {
            foreach (var item in model.Options)
            {
                var c = db.Options.Where(p => p.Name == item.Key).FirstOrDefault();
                if (c != null)
                {
                    c.Value = item.Value;
                }
            }

            var styleFile = Server.MapPath("/Content/Style.css");
            var newStyle = Server.MapPath("/Content/Themes/" + model.Options["BlogStyle"]);
            System.IO.File.Copy(newStyle, styleFile, true);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [NonAction]
        public SelectList GetStyleList(string blogStyle)
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            var stylePath = Server.MapPath("/Content/Themes");
            var listOfStyle = Directory.GetFiles(stylePath, "*.css", SearchOption.TopDirectoryOnly);
            foreach (var item in listOfStyle)
            {
                var currentStyle = Path.GetFileName(item);
                listItems.Add(new SelectListItem { Text = currentStyle, Value = currentStyle });
            }
            return new SelectList(listItems, "Value", "Text", blogStyle);
        }

    }
}