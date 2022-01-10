using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgrammerBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerBlog.Data.Concrete.EntityFramework.Mappings
{
    public class ArticleMap : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(a => a.Id);//p.k
            builder.Property(a => a.Id).ValueGeneratedOnAdd();//identity +1
            builder.Property(a => a.Title).HasMaxLength(100);
            builder.Property(a => a.Title).IsRequired();
            builder.Property(a => a.Content).IsRequired();
            builder.Property(a => a.Content).HasColumnType("NVARCHAR(MAX)");
            builder.Property(a => a.Date).IsRequired();
            builder.Property(a=>a.SeoAuthor).IsRequired();
            builder.Property(a => a.SeoAuthor).HasMaxLength(50);
            builder.Property(a => a.SeoDescription).HasMaxLength(150);
            builder.Property(a => a.SeoDescription).IsRequired();
            builder.Property(a => a.SeoTags).IsRequired();
            builder.Property(a => a.SeoTags).HasMaxLength(70);
            builder.Property(a => a.ViewsCount).IsRequired();
            builder.Property(a => a.CommentCount).IsRequired();
            builder.Property(a => a.Thumbnail).IsRequired();
            builder.Property(a => a.Thumbnail).HasMaxLength(250);
            builder.Property(a => a.CreatedByName).IsRequired();
            builder.Property(a => a.CreatedByName).HasMaxLength(50);
            builder.Property(a => a.ModifiedByName).IsRequired();
            builder.Property(a => a.ModifiedByName).HasMaxLength(50);
            builder.Property(a => a.CreatedDate).IsRequired();
            builder.Property(a => a.ModifiedDate).IsRequired();
            builder.Property(a => a.IsActive).IsRequired();
            builder.Property(a => a.IsDeleted).IsRequired();
            builder.Property(a => a.Note).HasMaxLength(500);
            builder.HasOne<Category>(a => a.Category).WithMany(c => c.Articles).HasForeignKey(a => a.CategoryId);
            //article da bir kategori , bir kategorinin birden fazla article 
            builder.HasOne<User>(a => a.User).WithMany(u => u.Articles).HasForeignKey(a=>a.UserId);
            builder.ToTable("Articles");

            builder.HasData(new Article
            {
                Id=1,
                CategoryId=1,
                Title="c#",
                Content="C# derslerine geldiniz",
                Thumbnail="Default.jpg",
                SeoDescription="c# yenilikler",
                SeoTags="C#",
                SeoAuthor="Doğukan Kazan",
                Date=DateTime.Now,
                IsActive=true,IsDeleted=false,CreatedByName="InitialCreate",CreatedDate=DateTime.Now,
                ModifiedByName="InitialCreate",ModifiedDate=DateTime.Now,
                Note="C# yenilikler",
                UserId=1,
                ViewsCount=100,
                CommentCount=1
            },
            new Article
            {
                Id = 2,
                CategoryId = 1,
                Title = "java",
                Content = "java derslerine geldiniz",
                Thumbnail = "Default.jpg",
                SeoDescription = "java yenilikler",
                SeoTags = "java",
                SeoAuthor = "Doğukan Kazan",
                Date = DateTime.Now,
                IsActive = true,
                IsDeleted = false,
                CreatedByName = "InitialCreate",
                CreatedDate = DateTime.Now,
                ModifiedByName = "InitialCreate",
                ModifiedDate = DateTime.Now,
                Note = "java yenilikler",
                UserId = 1,
                ViewsCount = 50,
                CommentCount = 5
            },
            new Article
            {
                Id = 3,
                CategoryId = 1,
                Title = "javascript",
                Content = "javascript derslerine geldiniz",
                Thumbnail = "Default.jpg",
                SeoDescription = "javascript yenilikler",
                SeoTags = "javascript",
                SeoAuthor = "Doğukan Kazan",
                Date = DateTime.Now,
                IsActive = true,
                IsDeleted = false,
                CreatedByName = "InitialCreate",
                CreatedDate = DateTime.Now,
                ModifiedByName = "InitialCreate",
                ModifiedDate = DateTime.Now,
                Note = "javascript yenilikler",
                UserId = 1,
                ViewsCount = 150,
                CommentCount = 15
            }
            );
        }
    }
}
