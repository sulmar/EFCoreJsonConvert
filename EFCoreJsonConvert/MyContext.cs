using EFCoreJsonConvert.Configurations;
using EFCoreJsonConvert.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreJsonConvert
{
    public class MyContext : DbContext
    {

        public DbSet<Person> Persons { get; set; }

        //public MyContext(DbContextOptions<MyContext> options)
        //    : base(options)
        //{

        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DemoDb;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new PersonConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
