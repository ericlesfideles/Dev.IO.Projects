using AppMvcBasic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IO.Data.Mappings
{
    public class AndressMapping : IEntityTypeConfiguration<AndressEntity>
    {
        public void Configure(EntityTypeBuilder<AndressEntity> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(e => e.Street)
                .HasColumnType("varchar(200)");
            
            builder.Property(e => e.Number)
                .HasColumnType("varchar(50)");
            
            builder.Property(e => e.Neighborhood)
                .HasColumnType("varchar(100)"); 
            
            builder.Property(e => e.Complement)
                .HasColumnType("varchar(200)");
            
            builder.Property(e => e.PostCode)
                .HasColumnType("varchar(8)");
            
            builder.Property(e => e.City)
                .HasColumnType("varchar(100)");

            builder.Property(e => e.State)
                .HasColumnType("varchar(50)");

            
            builder.ToTable("Andresses");
        }
    }
}
