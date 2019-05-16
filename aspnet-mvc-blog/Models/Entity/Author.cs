using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace aspnet_mvc_blog.Models.Entity
{
    public class Author : EntityBase
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Display(Name ="First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        [Required]
        public UserRole Role { get; set; }

        // Relations
        public ICollection<Entry> Entries { get; set; }

        public Author()
        {
            Entries = new List<Entry>();
        }
    }
    
    public enum UserRole : byte
    {
        Administrator,
        Editor,
        Writer
    }
}