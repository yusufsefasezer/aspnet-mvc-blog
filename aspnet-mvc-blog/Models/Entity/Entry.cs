using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace aspnet_mvc_blog.Models.Entity
{
    public class Entry : EntityBase
    {
        public int AuthorID { get; set; }
        [Required]
        public string Title { get; set; }
        public string Slug { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public EntryType Type { get; set; }
        [Required]
        public StatusType Status { get; set; }

        [Display(Name = "Comment")]
        public bool CommentStatus { get; set; }

        public DateTime UpdatedAt { get; set; }

        // Relations
        public Author Author { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        //public virtual ICollection<Comment> Comments { get; set; }

        public Entry()
        {
            Categories = new List<Category>();
            //Comments = new List<Comment>();
        }
    }

    public enum EntryType : byte
    {
        Post,
        Page,
        //Image,
        //Video
    }
    public enum StatusType : byte
    {
        Published,
        Pending,
        Draft,
        Private
    }
}