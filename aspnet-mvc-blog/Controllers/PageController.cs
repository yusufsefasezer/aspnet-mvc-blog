using aspnet_mvc_blog.Models.Entity;
using aspnet_mvc_blog.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace aspnet_mvc_blog.Controllers
{
    public class PageController : ControllerBase
    {
        [Route("page/{slug}")]
        public ActionResult Index(string slug)
        {
            EntryViewModel model = (from Entry in db.Entries
                                    where Entry.Type == EntryType.Page
                                    where Entry.Slug == slug
                                    where Entry.Status == StatusType.Published
                                    where Entry.CreatedAt < DateTime.Now
                                    select new EntryViewModel
                                    {
                                        Title = Entry.Title,
                                        Author = new LinkViewModel { Title = Entry.Author.FirstName, Slug = Entry.Author.ID.ToString() },
                                        Content = Entry.Content,
                                        CommentStatus = Entry.CommentStatus,
                                        Slug = Entry.Slug,
                                        DateTime = Entry.UpdatedAt
                                    }).FirstOrDefault();

            if (model == null) return HttpNotFound();

            return View(model);
        }
    }
}