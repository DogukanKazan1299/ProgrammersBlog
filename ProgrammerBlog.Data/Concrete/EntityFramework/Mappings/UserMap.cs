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
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd();
            builder.HasIndex(u=>u.Email).IsUnique();//sadece bir email
            builder.Property(u => u.Email).IsRequired();
            builder.Property(u => u.Email).HasMaxLength(50);
            builder.Property(u => u.UserName).IsRequired();
            builder.Property(u => u.UserName).HasMaxLength(20);
            builder.HasIndex(u => u.UserName).IsUnique();//tek username
            builder.Property(u => u.PasswordHash).IsRequired();
            builder.Property(u => u.PasswordHash).HasColumnType("VARBINARY(500)");
            builder.Property(u => u.Description).HasMaxLength(500);
            builder.Property(u => u.FirstName).IsRequired();
            builder.Property(u => u.FirstName).HasMaxLength(30);
            builder.Property(u => u.LastName).IsRequired();
            builder.Property(u => u.LastName).HasMaxLength(30);
            builder.Property(u => u.Picture).IsRequired();
            builder.Property(u => u.Picture).HasMaxLength(250);
            builder.HasOne<Role>(u => u.Role).WithMany(r => r.Users).HasForeignKey(u => u.RoleId);
            builder.Property(u => u.CreatedByName).IsRequired();
            builder.Property(u => u.CreatedByName).HasMaxLength(50);
            builder.Property(u => u.ModifiedByName).IsRequired();
            builder.Property(u => u.ModifiedByName).HasMaxLength(50);
            builder.Property(u => u.CreatedDate).IsRequired();
            builder.Property(u => u.ModifiedDate).IsRequired();
            builder.Property(u => u.IsActive).IsRequired();
            builder.Property(u => u.IsDeleted).IsRequired();
            builder.Property(u => u.Note).HasMaxLength(500);
            builder.ToTable("Users");

            builder.HasData(new User 
            {
                Id=1,
                RoleId=1,
                FirstName="Doğukan",
                LastName="Kazan",
                Email="kazand25@gmail.com",
                IsActive=true,
                IsDeleted=false,
                CreatedByName="InitialCreate",
                CreatedDate=DateTime.Now,
                ModifiedByName="InitialCreate",
                ModifiedDate=DateTime.Now,
                Description="İlk admin kullanıcı",
                Note="Admin kullanıcı",
                PasswordHash=Encoding.ASCII.GetBytes("21232f297a57a5a743894a0e4a801fc3"),//admin
                Picture= "https://e7.pngegg.com/pngimages/1021/858/png-clipart-laptop-computer-icons-computer-monitors-desktop-computers-laptop-black-laptop-computer-illustration-electronics-computer-thumbnail.png"
            });
        }
    }
}
