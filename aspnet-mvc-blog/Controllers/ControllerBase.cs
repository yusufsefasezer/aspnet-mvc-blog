using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using aspnet_mvc_blog.Models.Entity;
using aspnet_mvc_blog.DAL;
using aspnet_mvc_blog.Models.ViewModel;

namespace aspnet_mvc_blog.Controllers
{
    public class ControllerBase : Controller
    {
        public BlogContext db;

        public ControllerBase()
        {
            db = new BlogContext();
            foreach (var option in db.Options)
            {
                ViewData[option.Name] = option.Value;
            }

            ViewData["MenuItems"] = (from Pages in db.Entries
                                     where Pages.Type == EntryType.Page
                                     where Pages.Status == StatusType.Published
                                     where Pages.CreatedAt < DateTime.Now
                                     select new LinkViewModel { Title = Pages.Title, Slug = Pages.Slug }).ToList();

            ViewData["Categories"] = (from Categories in db.Categories
                                      select new LinkViewModel { Title = Categories.Name, Slug = Categories.Slug }).ToList();
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