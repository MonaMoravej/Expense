using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyCodeCamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCodeCamp.Data
{
    public class testDb : DbContext
    {
        private IConfigurationRoot _config;

        public testDb(DbContextOptions options, IConfigurationRoot config)
      : base(options)
        {
            _config = config;
        }

        public DbSet<test> tests { get; set; }
        public DbSet<test2> test2s { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config["Data:ConnectionString"]);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}
