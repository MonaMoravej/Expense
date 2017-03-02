using Data.Entities.Identity;
using Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Threading.Tasks;
using WebServices.Models;
using WebServices.Models.UserModel;

namespace WebServices.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IIdentityReadRepository<Language> _lRepository;
        private IIdentityReadRepository<Currency> _cRepository;
        private UserManager<User> _userManager;

        public UserController(UserManager<User> userManager,
            IIdentityReadRepository<Language> lRepository,
            IIdentityReadRepository<Currency> cRepository)
        {
            _userManager = userManager;
            _lRepository = lRepository;
            _cRepository = cRepository;
        }

        [HttpGet("Profile")]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var lanName = _lRepository.FindById(user.LanguageId).Name;
                var curName = _cRepository.FindById(user.CurrencyId).Name;
                var userModel = new UserProfile()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    BirthDate = user.BirthDate,
                    CurrencyName = curName,
                    LanguageName = lanName,
                    Gender = user.Gender, //.ToString(),
                    PhoneNumber = user.PhoneNumber,
                    Picture = user.Picture

                };


                return Ok(userModel);
            }
            catch(Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message }); }
            return BadRequest();
        }

        //userName and password not changeable
        [HttpPost("Profile")]
        public async Task<IActionResult> PostProfile([FromBody] UserProfile model)
        {
            try
            {
                var lanId = _lRepository.FindByName(model.LanguageName).Id;
                var curId = _cRepository.FindByName(model.CurrencyName).Id;
                var user = new User()
                {
                    UserName = User.Identity.Name,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    BirthDate = model.BirthDate,
                    CurrencyId = curId,
                    LanguageId = lanId,
                    Gender = model.Gender,
                    PhoneNumber = model.PhoneNumber,
                    Picture = model.Picture
                };

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded) { return Ok(); }
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message }); }
            return BadRequest();
        }

        //just update password
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> PostChangePassword ([FromBody] ChangePasswordModel model)
        {
            try {
                if (ModelState.IsValid)
                {
                   
                    var user = await _userManager.FindByNameAsync(User.Identity.Name);
                    if (user != null)
                    {
                        //var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                        //if (result.Succeeded)
                        //{
                        //    await _si.SignInAsync(user, isPersistent: false);

                        //}
                        return Ok();
                    }
                    ModelState.AddModelError(string.Empty, "user not found");
                    return NotFound(model);
                       
                    
                }

                return NotFound(model);
            }
            catch(Exception ex) { return NotFound(model); }
            return NotFound(model);
        }

        //delete user and signout 
    }
}
