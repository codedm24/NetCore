using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDemo
{
    public class MenuCardConfiguration : IEntityTypeConfiguration<MenuCard>
    {
        public void Configure(EntityTypeBuilder<MenuCard> builder)
        { 
            builder.ToTable("MenuCards")
                .HasKey(c => c.MenuCardId);

            builder.Property(c => c.MenuCardId)
                .ValueGeneratedOnAdd();

            builder.Property(c => c.Title)
                .HasMaxLength(50);

            builder.HasMany(c => c.Menus).WithOne(c => c.MenuCard);
        }
    }
}
