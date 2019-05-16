namespace aspnet_mvc_blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Author",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 255),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(maxLength: 100),
                        Role = c.Byte(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Email, unique: true);
            
            CreateTable(
                "dbo.Entry",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AuthorID = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 200),
                        Slug = c.String(nullable: false, maxLength: 200),
                        Content = c.String(nullable: false),
                        Type = c.Byte(nullable: false),
                        Status = c.Byte(nullable: false),
                        CommentStatus = c.Boolean(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Author", t => t.AuthorID, cascadeDelete: true)
                .Index(t => t.AuthorID)
                .Index(t => t.Slug, unique: true);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ParentID = c.Int(),
                        Name = c.String(nullable: false, maxLength: 200),
                        Slug = c.String(nullable: false, maxLength: 200),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Category", t => t.ParentID)
                .Index(t => t.ParentID)
                .Index(t => t.Slug, unique: true);
            
            CreateTable(
                "dbo.Option",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Value = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.CategoryEntry",
                c => new
                    {
                        EntryID = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EntryID, t.CategoryID })
                .ForeignKey("dbo.Entry", t => t.EntryID, cascadeDelete: true)
                .ForeignKey("dbo.Category", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.EntryID)
                .Index(t => t.CategoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CategoryEntry", "CategoryID", "dbo.Category");
            DropForeignKey("dbo.CategoryEntry", "EntryID", "dbo.Entry");
            DropForeignKey("dbo.Category", "ParentID", "dbo.Category");
            DropForeignKey("dbo.Entry", "AuthorID", "dbo.Author");
            DropIndex("dbo.CategoryEntry", new[] { "CategoryID" });
            DropIndex("dbo.CategoryEntry", new[] { "EntryID" });
            DropIndex("dbo.Option", new[] { "Name" });
            DropIndex("dbo.Category", new[] { "Slug" });
            DropIndex("dbo.Category", new[] { "ParentID" });
            DropIndex("dbo.Entry", new[] { "Slug" });
            DropIndex("dbo.Entry", new[] { "AuthorID" });
            DropIndex("dbo.Author", new[] { "Email" });
            DropTable("dbo.CategoryEntry");
            DropTable("dbo.Option");
            DropTable("dbo.Category");
            DropTable("dbo.Entry");
            DropTable("dbo.Author");
        }
    }
}
