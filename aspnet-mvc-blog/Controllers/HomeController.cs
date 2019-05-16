using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using aspnet_mvc_blog.Models.Entity;
using aspnet_mvc_blog.Models.ViewModel;

namespace aspnet_mvc_blog.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index(int page = 1)
        {
            if (page < 1) return HttpNotFound();

            EntryListViewModel model = new EntryListViewModel();
            var PostPerPage = Convert.ToInt32(ViewData["EntryPerPage"]);
            var entries = from Entries in db.Entries
                          where Entries.Type == EntryType.Post
                          where Entries.Status == StatusType.Published
                          where Entries.CreatedAt < DateTime.Now
                          join Author in db.Authors on Entries.AuthorID equals Author.ID
                          select Entries;

            model.CurrentPage = page;

            var EntryCount = entries.Count();
            var TotalPage = (double)EntryCount / PostPerPage;
            model.PageCount = (int)Math.Ceiling(TotalPage);

            var skip = (page - 1) * PostPerPage;
            model.Entries = (from e in entries
                             select new EntryViewModel
                             {
                                 ID = e.ID,
                                 Title = e.Title,
                                 Author = new LinkViewModel { Title = e.Author.FirstName, Slug = e.Author.ID.ToString() },
                                 Content = e.Content,
                                 Slug = e.Slug,
                                 DateTime = e.UpdatedAt,
                                 Categories = (from Categories in e.Categories
                                               select new LinkViewModel { Title = Categories.Name, Slug = Categories.Slug })
                                               .ToList()
                             })
                             .OrderByDescending(p => p.ID)
                             .Skip(skip)
                             .Take(PostPerPage)
                             .ToList();

            return View(model);
        }
    }
}