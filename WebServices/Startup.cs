using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Data.DbContexts;
using Data;
using Data.Repositories;
using Data.Entities.Expense;
using Data.DatabaseInitializer;
using Newtonsoft.Json;
using Data.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using WebServices.Businesses;

namespace WebServices
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            _config = builder.Build();


        }

        public IConfigurationRoot _config { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //set up dependency injection
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_config);
            services.AddDbContext<ExpenseDb>(ServiceLifetime.Scoped);
          
            services.AddScoped(typeof(IAccountRepository), typeof(AccountRepository));
            //services.AddScoped(typeof(IconRepository), typeof(IconRepository));
            //services.AddScoped(typeof(BudgetRepository), typeof(BudgetRepository));
            //services.AddScoped(typeof(CategoryRepository), typeof(CategoryRepository));
            //services.AddScoped(typeof(PayeeRepository), typeof(PayeeRepository));
            services.AddScoped(typeof(ITransactionRepository), typeof(TransactionRepository));

            services.AddScoped(typeof(AccountBusiness));

            services.AddDbContext<IdentityDb>(ServiceLifetime.Scoped);
            services.AddIdentity<User, IdentityRole<Guid>>().
              AddEntityFrameworkStores<IdentityDb, Guid>();

            services.AddScoped(typeof(IIdentityReadRepository<>),typeof(IdentityReadRepository<>));

            //for Url mapping
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.Configure<IdentityOptions>(config =>
            config.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents()
            {
                OnRedirectToLogin = (ctx) =>
                {
                    if (ctx.Request.Path.StartsWithSegments("/api")
                    && ctx.Response.StatusCode == (int)HttpStatusCode.OK)
                    {
                        ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    }
                    return Task.CompletedTask;
                },
                OnRedirectToAccessDenied = (ctx) =>
                {
                    if (ctx.Request.Path.StartsWithSegments("/api")
                    && ctx.Response.StatusCode == (int)HttpStatusCode.OK)
                    {
                        ctx.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    }
                    return Task.CompletedTask;
                }
            });

            services.AddTransient<UserInitializer>();
            services.AddAutoMapper();


            //error : defulat 
            //ignore : because we know they are exist becauese of navigation property 
            //        and we don't want to serialize for client
            //Serialize: may be dangrouse
            services.AddMvc().AddJsonOptions(
                opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            );


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //use components for request pipeline
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory ,UserInitializer userInit)
        {

            loggerFactory.AddConsole(_config.GetSection("Logging"));
            loggerFactory.AddDebug();


            app.UseIdentity();

            app.UseMvc();

            app.ApplicationServices.GetRequiredService<ExpenseDb>().Seed();
            app.ApplicationServices.GetRequiredService<IdentityDb>().Seed();

            userInit.AddUser(app.ApplicationServices.GetRequiredService<IdentityDb>()).Wait();

        }
    }
}
