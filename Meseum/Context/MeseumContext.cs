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
        }
       // public DbSet<User> users { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<NewsEvent> NewsEvents { get; set; }
        public DbSet<Posture> Postures { get; set; }
        public DbSet<Files> Files { get; set; }

    }
}