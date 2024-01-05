using Microsoft.EntityFrameworkCore;
using A2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A2.Dtos;

namespace A2.Data
{
    public class A2DbContext: DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public A2DbContext(DbContextOptions<A2DbContext> options) : base(options) { }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=A2Database.sqlite");
        }
    }
    
}