using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aspnet_mvc_blog.Models.ViewModel
{
    public class EntryViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Slug { get; set; }

        public bool CommentStatus { get; set; }
        public LinkViewModel Author { get; set; }
        public List<LinkViewModel> Categories { get; set; }
        public DateTime DateTime { get; set; }
    }
}