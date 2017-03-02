using Data.DbContexts;
using Data.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebServices.Models;
using Data.Repositories;
using WebServices.Models.UserModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebServices.Controllers
{
    public class AuthenticationController : Controller
    {
        private IIdentityReadRepository<Language> _lRepository;
        private IIdentityReadRepository<Currency> _cRepository;
        private SignInManager<User> _signInManager;
        private UserManager<User> _userManager;
        private IPasswordHasher<User> _passwordHasher;
        public AuthenticationController(IIdentityReadRepository<Language> lRepository,
            IIdentityReadRepository<Currency> cRepository,
            SignInManager<User> signInManager, 
            UserManager<User> userManager,
            IPasswordHasher<User> passwordHasher)
        {
            _lRepository = lRepository;
            _cRepository = cRepository;
            _signInManager = signInManager;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("api/Authentication/SignIn")]
        //[ValidateModel]
        public async Task<IActionResult> PostSignIn([FromBody] CredentialModel model)
        {
            try
            {
                var result = await SignIn(model);
                if (result.Succeeded) return Ok();
                //else return Unauthorized();
            }
            catch (Exception ex)
            {

            }
            return BadRequest();
        }

        [Authorize]
        [HttpGet("api/Authentication/SignOut")]
        public async Task<IActionResult> Get()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "Couldn't logOut." });
            }
            return BadRequest();
        }

        [HttpPost("api/Authentication/SignUp")]
        public async Task<IActionResult> PostSignUp([FromBody] UserProfile model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //from repository currency
                    //from repository: language
                    var result = await SignUp(model);
                   
                    if (result.Succeeded) return Ok();

                    else
                    {
                        string messages = "";
                        foreach (var e in result.Errors) { messages += " " + e.Description; }
                        return StatusCode((int)HttpStatusCode.NotAcceptable, new { message = messages });
                    }
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.NotAcceptable, new { message = "Couldn't SignUp." });

                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message });

            }
            return BadRequest();

        }


        [HttpPost("api/Authentication/SignUpAndSignIn")]
        public async Task<IActionResult> PostSignUpSignIn([FromBody] UserProfile model)
        {
            try
            {
                var resultSignUp = await SignUp(model);
                if (!resultSignUp.Succeeded)
                {
                    string messages = "";
                    foreach (var e in resultSignUp.Errors) { messages += " " + e.Description; }
                    return StatusCode((int)HttpStatusCode.NotAcceptable, new { message = messages });

                }
                else
                {
                    var credential = new CredentialModel() { UserName = model.UserName, Password = model.Password };

                    var resultSigIn = await SignIn(credential);
                    if (!resultSigIn.Succeeded)
                    {
                        return Unauthorized();
                    }
                    else
                    {
                        return Ok();
                    }
                }
                
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message });

            }
            return BadRequest();

        }


        //private Members
        private async Task<IdentityResult> SignUp(UserProfile model)
        {
            
            var lanId = _lRepository.FindByName(model.LanguageName).Id;
            var curId = _cRepository.FindByName(model.CurrencyName).Id;
            var user = new User()
            {
                UserName = model.UserName,
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
            var result = await _userManager.CreateAsync(user, model.Password);
            return result;
        } 

        private async Task<Microsoft.AspNetCore.Identity.SignInResult> SignIn(CredentialModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
            return result;
        }

        [HttpPost("api/Authentication/Token")]
        public async Task<IActionResult> CreateToken([FromBody] UserProfile model)
        {
            try {
                var user =await _userManager.FindByNameAsync(model.UserName);
                if (user == null) return BadRequest("user is not valid!");
                var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);
                if(! (result==PasswordVerificationResult.Success) )  return BadRequest("password is incorrect!");

                var Claims = new[] { new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())  };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("VERYLONGKEYISSECURE"));

                var Creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
                
                var token = new JwtSecurityToken(
                    issuer: "http://localhost:57053/",
                    audience: "http://localhost:57053/",
                    claims: Claims,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    signingCredentials:Creds
                    );

                return Ok(new {token=new JwtSecurityTokenHandler().WriteToken(token),expiration=token.ValidTo });
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message }); }
            return BadRequest();
        }
    }
}
