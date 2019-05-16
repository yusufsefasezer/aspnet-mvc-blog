using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aspnet_mvc_blog.Areas.Admin.ViewModels
{
    public class CategoryViewModel
    {
        public aspnet_mvc_blog.Models.Entity.Category Category { get; set; }
        public List<aspnet_mvc_blog.Models.Entity.Category> Categories { get; set; }
    }
}