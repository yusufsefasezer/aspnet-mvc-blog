using aspnet_mvc_blog.Models.Entity;
using aspnet_mvc_blog.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace aspnet_mvc_blog.Controllers
{
    public class AuthorController : ControllerBase
    {
        [Route("author/{id}", Order = 0)]
        public ActionResult Index(int? id, int page = 1)
        {
            if (id == null || page < 1) return HttpNotFound();

            AuthorViewModel model = new AuthorViewModel();
            var PostPerPage = Convert.ToInt32(ViewData["EntryPerPage"]);
            var entries = from Entries in db.Entries
                          where Entries.Type == EntryType.Post
                          where Entries.AuthorID == id
                          where Entries.Status == StatusType.Published
                          where Entries.CreatedAt < DateTime.Now
                          join Author in db.Authors on Entries.AuthorID equals Author.ID
                          select Entries;

            model.CurrentPage = page;

            var EntryCount = entries.Count();
            if (EntryCount > 0) model.Author = db.Authors.Where(p => p.ID == id).FirstOrDefault().FullName;

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