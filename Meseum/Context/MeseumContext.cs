using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Meseum.Models;

namespace Meseum.Context
{
    public class MeseumContext : DbContext
    {
        public MeseumContext()
            : base("DefaultConnection")
        {
            //this.Configuration.LazyLoadingEnabled = false;
            //this.Configuration.ProxyCreationEnabled = false;
        }
       // public DbSet<User> users { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<NewsEvent> NewsEvents { get; set; }
        public DbSet<Posture> Postures { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Events> Events { get; set; }
        public DbSet<Gallery> Gallery { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<Queries> Queries { get; set; }
        public DbSet<Files> Files { get; set; }
        public DbSet<ImageFile> ImageFile { get; set; }
        public DbSet<Banner> Banners { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Gallery>()
        //        .HasOptional<Files>(b => b.f)
        //        .WithMany()
        //        .WillCascadeOnDelete(false);
        //}
    }
}