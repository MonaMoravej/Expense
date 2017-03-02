using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test2DbContextWithShareData.Entities;

namespace Test2DbContextWithShareData.DbContexts
{
    public class Entity1DbContext:DbContext
{
        private IConfigurationRoot _config;
        public Entity1DbContext(DbContextOptions<Entity1DbContext> options,IConfigurationRoot config) : base(options)
        {
            _config = config;
        }

        protected  override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config["Data:ConnectionString"]);
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("one");
            base.OnModelCreating(modelBuilder);
            modelBuilder.Ignore(typeof(Entity2));
        }


        public DbSet<Entity1> Entities1 { get; set; }
        public DbSet<Entity2> Entities2 { get; private set; }
    }
}
