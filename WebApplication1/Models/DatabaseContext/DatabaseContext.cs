using WebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.DatabaseContext
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Notes> Notes { get; set; }
        public DbSet<Daily> Daily { get; set; }
        public DbSet<Group> Group{ get; set; }
        public DbSet<UserGroup> UserGroups{ get; set; }
        public DbSet<Category> Category { get; set; }
        public DatabaseContext() : base("UserDbContext")
        {
            Database.SetInitializer(new MyInitializer());
        }
    }
}