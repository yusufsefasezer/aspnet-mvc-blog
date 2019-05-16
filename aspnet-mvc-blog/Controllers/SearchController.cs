using aspnet_mvc_blog.Models.Entity;
using aspnet_mvc_blog.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace aspnet_mvc_blog.Controllers
{
    public class SearchController : ControllerBase
    {
        public ActionResult Index(string q, int page = 1)
        {
            if (string.IsNullOrWhiteSpace(q)) return HttpNotFound();
            if (page < 1) page = 1;

            SearchViewModel model = new SearchViewModel();
            var PostPerPage = Convert.ToInt32(ViewData["EntryPerPage"]);
            var entries = from Entries in db.Entries
                          where Entries.Type == EntryType.Post
                          where Entries.Title.Contains(q)
                          where Entries.Status == StatusType.Published
                          where Entries.CreatedAt < DateTime.Now
                          join Author in db.Authors on Entries.AuthorID equals Author.ID
                          select Entries;

            model.SearchTerm = q;
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
                                               select new LinkViewModel { Title = Categories.Name, Slug = Categories.Slug }).ToList()
                             })
                             .OrderByDescending(p => p.ID)
                             .Skip(skip)
                             .Take(PostPerPage)
                             .ToList();

            return View(model);
        }
    }
}