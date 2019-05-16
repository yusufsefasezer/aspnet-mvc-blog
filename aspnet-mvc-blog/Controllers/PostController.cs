using aspnet_mvc_blog.Models.Entity;
using aspnet_mvc_blog.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace aspnet_mvc_blog.Controllers
{
    public class PostController : ControllerBase
    {
        [Route("{slug:regex(^(?!search$))}")]
        public ActionResult Index(string slug)
        {
            EntryViewModel model = (from Entry in db.Entries
                                    where Entry.Type == EntryType.Post
                                    where Entry.Slug == slug
                                    where Entry.Status == StatusType.Published
                                    where Entry.CreatedAt < DateTime.Now
                                    join Author in db.Authors on Entry.AuthorID equals Author.ID

                                    select new EntryViewModel
                                    {
                                        Title = Entry.Title,
                                        Author = new LinkViewModel { Title = Entry.Author.FirstName, Slug = Entry.Author.ID.ToString() },
                                        Content = Entry.Content,
                                        Slug = Entry.Slug,
                                        CommentStatus = Entry.CommentStatus,
                                        DateTime = Entry.UpdatedAt,
                                        Categories = Entry.Categories.Select(p => new LinkViewModel { Title = p.Name, Slug = p.Slug }).ToList()
                                    }).FirstOrDefault();

            if (model == null) return HttpNotFound();

            return View(model);
        }
    }
}