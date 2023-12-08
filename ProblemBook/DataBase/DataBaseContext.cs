using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProblemBook.DataBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ProblemBook.DataBase
{
    internal class DataBaseContext : DbContext
    {
        private DataBaseContext()
        {
            Database.EnsureCreated();
        }

        public static DataBaseContext Instance { get; } = new();

        public DbSet<Problem> Problems { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=problemBook.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Problem>().ToTable("Problem");
        }
    }
}
