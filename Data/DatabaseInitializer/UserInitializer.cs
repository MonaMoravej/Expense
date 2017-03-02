using Data.DbContexts;
using Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.DatabaseInitializer
{

    //just for test , when we publish db must haven't any user in it!!!
    public class UserInitializer
    {
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole<Guid>> _roleMnager;
        public UserInitializer(UserManager<User> userManager,
                                   RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;
            _roleMnager = roleManager;
        }

        public async Task AddUser(IdentityDb db)
        {

            if (!_userManager.Users.Any())
            {
               
                Guid lanId = db.Languages.FirstOrDefault(l => l.Name == "English").Id;
                Guid CurId = db.Currencies.FirstOrDefault(c => c.Name == "RM").Id;

                var result = await _userManager.CreateAsync(
                    new User()
                    {
                        FirstName = "Mona",
                        LastName = "Moravej",
                        UserName = "mona.moravej@gmail.com",
                        Email = "mona.moravej@gmail.com",
                        LanguageId = lanId,
                        CurrencyId = CurId
                    }, "Mona1!");

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException("Failed to build user and roles");
                }
            }
        }
    }

}
