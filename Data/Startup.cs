using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Data.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Data.Repositories;
using Data.Entities.Expense;


namespace Data
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json");

            _config = builder.Build();
        }

        IConfigurationRoot _config;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_config);


            services.AddDbContext<ExpenseDb>(ServiceLifetime.Scoped);
            services.AddDbContext<IdentityDb>()
              .AddIdentity<User, IdentityRole<Guid>>();
            
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

        




        }


     

    }
}
