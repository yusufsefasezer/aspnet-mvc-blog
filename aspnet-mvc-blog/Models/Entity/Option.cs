using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aspnet_mvc_blog.Models.Entity
{
    public class Option : EntityBase
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}