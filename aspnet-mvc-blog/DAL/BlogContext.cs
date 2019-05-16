using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using aspnet_mvc_blog.Models.Entity;

namespace aspnet_mvc_blog.DAL
{
    public class BlogContext : DbContext
    {
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        //public DbSet<Comment> Comments { get; set; }
        public DbSet<Option> Options { get; set; }

        public BlogContext() : base("BlogConnection") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            RelationBuilder(modelBuilder);
            AuthorBuilder(modelBuilder);
            CategoryBuilder(modelBuilder);
            EntryBuilder(modelBuilder);
            //CommentBuilder(modelBuilder);
            OptionBuilder(modelBuilder);
        }

        private static void RelationBuilder(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entry>()
                        .HasMany<Category>(p => p.Categories)
                        .WithMany(p => p.Entries)
                        .Map(ret =>
                        {
                            ret.MapLeftKey("EntryID");
                            ret.MapRightKey("CategoryID");
                            ret.ToTable("CategoryEntry");
                        });

            //modelBuilder.Entity<Comment>()
            //            .HasRequired<Entry>(p => p.Entry)
            //            .WithMany(p => p.Comments)
            //            .HasForeignKey(x => x.EntryID);

            modelBuilder.Entity<Entry>()
                        .HasRequired<Author>(p => p.Author)
                        .WithMany(p => p.Entries)
                        .HasForeignKey(x => x.AuthorID);

            //modelBuilder.Entity<Category>()
            //    .HasRequired<Category>(p => p.Parent)
            //    .WithMany(p => p.SubCategories)
            //    .HasForeignKey(p => p.ParentID);

            //modelBuilder.Entity<Comment>()
            //    .HasRequired<Comment>(p => p.Parent)
            //    .WithMany(p => p.SubComments)
            //    .HasForeignKey(p => p.ParentID);
        }

        private static void CategoryBuilder(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(p => p.Name)
                .HasMaxLength(200)
                .IsRequired();

            modelBuilder.Entity<Category>()
                .Property(p => p.Slug)
                .HasMaxLength(200)
                .IsRequired();

            modelBuilder.Entity<Category>()
                .HasIndex(p => p.Slug)
                .IsUnique();
        }

        private static void AuthorBuilder(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .Property(p => p.Email)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Author>()
                .Property(p => p.Password)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Author>()
                .Property(p => p.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Author>()
                .Property(p => p.LastName)
                .HasMaxLength(100);

            modelBuilder.Entity<Author>()
                .HasIndex(p => p.Email)
                .IsUnique();
        }

        private static void OptionBuilder(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Option>()
                .Property(p => p.Name)
                .HasMaxLength(50);

            modelBuilder.Entity<Option>()
                .HasIndex(p => p.Name)
                .IsUnique();
        }

        private static void CommentBuilder(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                .Property(p => p.Author)
                .HasMaxLength(100);

            modelBuilder.Entity<Comment>()
                .Property(p => p.Email)
                .HasMaxLength(100);

            modelBuilder.Entity<Comment>()
                .Property(p => p.URL)
                .HasMaxLength(100);

            modelBuilder.Entity<Comment>()
                .Property(p => p.IP)
                .HasMaxLength(100);
        }

        private static void EntryBuilder(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entry>()
                .Property(p => p.Title)
                .HasMaxLength(200)
                .IsRequired();

            modelBuilder.Entity<Entry>()
                .Property(p => p.Slug)
                .HasMaxLength(200)
                .IsRequired();

            modelBuilder.Entity<Entry>()
                .HasIndex(p => p.Slug)
                .IsUnique();
        }
    }
}