using AppMvcBasic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dev.IO.Data.Context
{
    public class MyDbContext: DbContext
    {

        public MyDbContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<ProductEntity> Products { get; set; }

        public DbSet<AndressEntity> Andresses { get; set; }

        public DbSet<SupplierEntity> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
                     

            base.OnModelCreating(modelBuilder);
        }

    }
}
