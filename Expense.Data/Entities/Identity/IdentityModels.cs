using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using Expense.Entities;
using Expense.Data.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Expense.Data.DbContexts;
using Expense.Entities.Expense;

namespace Expense.Data.Entities.Identity
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser<Guid, CustomUserLogin, CustomUserRole, CustomUserClaim> 
    {
        public DateTime? BirthDate { get; set; }

        [Required]
        public HumanGender Gender { get; set; }


        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        public byte[] Picture { get; set; }


        // navigation properties
        [Required]
        public Guid CurrencyId { get; set; }

        [Required]
        public Guid LanguageId { get; set; }

        public Currency Currency { get; set; }

        public Language Language { get; set; }

        //navigation to Expense
        public ICollection<Account> Accounts { get; set; }
        public ICollection<Budget> Budgets { get; set; }

        public ICollection<Category> Categories { get; set; }

        public ICollection<Payee> Payees { get; set; }

        public ICollection<Transaction> Transactions { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser,Guid> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class CustomUserRole : IdentityUserRole<Guid> { }
    public class CustomUserClaim : IdentityUserClaim<Guid> { }
    public class CustomUserLogin : IdentityUserLogin<Guid> { }

  

    public class CustomRole : IdentityRole<Guid, CustomUserRole>
    {
        public CustomRole() { Id = Guid.NewGuid(); }
        public CustomRole(string name) { Name = name; }
    }

    public class CustomUserStore : UserStore<ApplicationUser, CustomRole, Guid,
        CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(IdentityDb context)
            : base(context)
        {
        }
    }

    public class CustomRoleStore : RoleStore<CustomRole, Guid, CustomUserRole>
    {
        public CustomRoleStore(IdentityDb context)
            : base(context)
        {
        }
    }
}