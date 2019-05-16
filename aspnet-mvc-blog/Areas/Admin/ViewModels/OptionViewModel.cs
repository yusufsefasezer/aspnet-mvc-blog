using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using aspnet_mvc_blog.Models.Entity;

namespace aspnet_mvc_blog.Areas.Admin.ViewModels
{
    public class OptionViewModel
    {
        public Dictionary<string, string> Options { get; set; }
        public SelectList Categories { get; set; }
    }
}