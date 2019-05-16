using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aspnet_mvc_blog.Models.Entity
{
    public class Category : EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? ParentID { get; set; }
        public virtual Category Parent { get; set; }
        //public virtual ICollection<Category> Parent { get; set; }

        [Required]
        public string Name { get; set; }

        public string Slug { get; set; }
        public virtual ICollection<Category> SubCategories { get; set; }

        // Relations
        public virtual ICollection<Entry> Entries { get; set; }
    }
}