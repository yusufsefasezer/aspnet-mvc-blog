using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace aspnet_mvc_blog.Models.Entity
{
    public class Comment : EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? ParentID { get; set; }
        public Comment Parent { get; set; }
        public int EntryID { get; set; }
        public string Author { get; set; }
        public string Email { get; set; }
        public string URL { get; set; }
        public string IP { get; set; }
        public string Content { get; set; }
        public bool CommentStatus { get; set; }
        public ICollection<Comment> SubComments { get; set; }

        // Relations
        public Entry Entry { get; set; }
    }
}