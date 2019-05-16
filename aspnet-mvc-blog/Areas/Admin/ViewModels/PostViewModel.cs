using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aspnet_mvc_blog.Areas.Admin.ViewModels
{
    public class PostViewModel
    {
        public aspnet_mvc_blog.Models.Entity.Entry Entry { get; set; }

        public List<int> SelectedCategories { get; set; }
        public List<aspnet_mvc_blog.Models.Entity.Category> Categories { get; set; }
    }
}