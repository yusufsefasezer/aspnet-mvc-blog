using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using aspnet_mvc_blog.Models.ViewModel;

namespace aspnet_mvc_blog.Areas.Admin.Controllers
{

    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            UserID = Convert.ToInt32(HttpContext.User.Identity.Name);
            ViewModels.OverviewViewModel model = new ViewModels.OverviewViewModel();
            model.PostCount = db.Entries.Where(p => p.Type == Models.Entity.EntryType.Post).Count();
            model.PageCount = db.Entries.Where(p => p.Type == Models.Entity.EntryType.Page).Count();
            model.CategoryCount = db.Categories.Count();
            model.AuthorCount = db.Authors.Count();
            model.Posts = (from RecentPosts in db.Entries
                           where RecentPosts.Type == Models.Entity.EntryType.Post
                           where RecentPosts.AuthorID == UserID
                           orderby RecentPosts.ID descending
                           select new EntryViewModel
                           {
                               Title = RecentPosts.Title,
                               DateTime = RecentPosts.CreatedAt,
                               Categories = RecentPosts.Categories.Select(c => new LinkViewModel { Title = c.Name, Slug = c.Slug }).ToList()
                           })
                           .ToList();

            model.Category = (from C in db.Categories
                              select new SelectListItem { Value = C.ID.ToString(), Text = C.Name })
                                .ToList();

            return View(model);
        }
    }
}