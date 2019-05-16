namespace aspnet_mvc_blog.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<aspnet_mvc_blog.DAL.BlogContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(aspnet_mvc_blog.DAL.BlogContext context)
        {

            context.Options.AddOrUpdate(
                new Models.Entity.Option { Name = "BlogTitle", Value = "Yusuf SEZER" },
                new Models.Entity.Option { Name = "BlogDescription", Value = "ASP.NET MVC Blog" },
                new Models.Entity.Option { Name = "AboutMe", Value = @"<h5>Yusuf SEZER</h5>" },
                new Models.Entity.Option { Name = "EntryPerPage", Value = "5" },
                new Models.Entity.Option { Name = "CommentSystemCode", Value = string.Empty },
                new Models.Entity.Option { Name = "BlogStyle", Value = "Default.css" }
            );

            Models.Entity.Author author = new Models.Entity.Author
            {
                ID = 1,
                Email = "yusufsezer@mail.com",
                Password = "123456",
                FirstName = "Yusuf",
                LastName = "SEZER",
                CreatedAt = DateTime.Now,
                Role = Models.Entity.UserRole.Administrator
            };
            context.Authors.Add(author);

            Models.Entity.Category category = new Models.Entity.Category
            {
                ID = 1,
                Name = "General",
                Slug = "general",
                CreatedAt = DateTime.Now
            };
            context.Categories.Add(category);

            Models.Entity.Entry entry = new Models.Entity.Entry
            {
                ID = 1,
                Title = "Lorem ipsum dolor sit amet",
                Slug = "lorem-ipsum-dolor-sit-amet",
                Content = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper ultricies nisi. Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc, blandit vel, luctus pulvinar, hendrerit id, lorem. Maecenas nec odio et ante tincidunt tempus. Donec vitae sapien ut libero venenatis faucibus. Nullam quis ante. Etiam sit amet orci eget eros faucibus tincidunt. Duis leo. Sed fringilla mauris sit amet nibh. Donec sodales sagittis magna. Sed consequat, leo eget bibendum sodales, augue velit cursus nunc,",
                AuthorID = author.ID,
                Categories = { category },
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = Models.Entity.StatusType.Published,
                CommentStatus = true,
                Type = Models.Entity.EntryType.Post
            };
            context.Entries.Add(entry);

            Models.Entity.Entry entry1 = new Models.Entity.Entry
            {
                ID = 2,
                Title = "Sed ut perspiciatis unde",
                Slug = "sed-ut-perspiciatis-unde",
                Content = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur? At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere",
                AuthorID = author.ID,
                Categories = { category },
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = Models.Entity.StatusType.Published,
                CommentStatus = true,
                Type = Models.Entity.EntryType.Post
            };
            context.Entries.Add(entry1);

            Models.Entity.Entry page = new Models.Entity.Entry
            {
                Title = "About",
                Slug = "about",
                Content = "About page",
                AuthorID = author.ID,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = Models.Entity.StatusType.Published,
                Type = Models.Entity.EntryType.Page
            };
            context.Entries.Add(page);

            context.SaveChanges();
        }
    }
}
