using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aspnet_mvc_blog.Models.ViewModel
{
    public class SearchViewModel : EntryListViewModel
    {
        public string SearchTerm { get; set; }
    }
}