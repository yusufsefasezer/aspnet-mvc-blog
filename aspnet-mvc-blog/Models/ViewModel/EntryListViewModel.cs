using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aspnet_mvc_blog.Models.ViewModel
{
    public class EntryListViewModel
    {
        public List<EntryViewModel> Entries;
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }

    }
}