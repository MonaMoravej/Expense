using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test2DbContextWithShareData.Entities;

namespace Test2DbContextWithShareData.DbContexts
{
    public class Entity2DbContext:DbContext
    {
        private IConfigurationRoot _config;
        public Entity2DbContext(DbContextOptions<Entity2DbContext> options, IConfigurationRoot config) : base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config["Data:ConnectionString"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("two");
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Entity1>().ToTable("Entities1", "one");

        }


        public DbSet<Entity2> Entities2 { get; set; }
        public DbSet<Entity1> Entities1 { get; private set; }

    }
}
