using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aspnet_mvc_blog.Models.ViewModel
{
    public class AuthorViewModel : EntryListViewModel
    {
        public string Author { get; set; }
    }
}