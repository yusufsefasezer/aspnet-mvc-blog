using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace aspnet_mvc_blog.Areas.Admin.ViewModels
{
    public class OverviewViewModel
    {
        public int PostCount { get; set; }
        public int PageCount { get; set; }
        public int CategoryCount { get; set; }
        public int AuthorCount { get; set; }
        public List<aspnet_mvc_blog.Models.ViewModel.EntryViewModel> Posts { get; set; }
        public List<SelectListItem> Category { get; set; }
    }
}