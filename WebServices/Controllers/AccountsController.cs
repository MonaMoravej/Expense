
using AutoMapper;
using Data.Entities.Expense;
using Data.Entities.Identity;
using Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using WebServices.Businesses;
using WebServices.Models.AccountModel;
using WebServices.Filters;

namespace WebServices.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ValidateModel]
    // [Route("api/User/{UserId}/[controller]")]

        //fir all of them ,BadRequest ,UnAuthorize , NotFound(for one entity) ,Duplicate, DeleteValidation and ModelState
    public class AccountsController : BaseController
    {
        private IAccountRepository _repository;
        private UserManager<User> _userManager;
        private IMapper _mapper;
        private AccountBusiness _accountBusiness;
        private ITransactionRepository _transactionRepository;
        public AccountsController(IAccountRepository repository,
            UserManager<User> userManager,
            IMapper mapper,
            AccountBusiness accountBusiness,
            ITransactionRepository transactionRepository)
        {
            _repository = repository;
            _userManager = userManager;
            _mapper = mapper;
            _accountBusiness = accountBusiness;
            _transactionRepository = transactionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                IEnumerable<Account> results = _repository.All(user.Id);
                
                return Ok(_mapper.Map<IEnumerable<AccountModel>>(results));
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message }); }
            return BadRequest();

        }

        //if we don't find return notFound
        [HttpGet("{Key}", Name = "Get")]
        public async Task<IActionResult> Get(string Key)
        {
            //
           // Request.Headers.ContainsKey("If-None-Match")

            try
            {

                Account result;
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (Request.Query.ContainsKey("IncludeTransactions")) //IncludeTransactions
                {
                    result = _accountBusiness.FindAccountByKeyIncludeTransactions(user.Id, Key);

                }
                else
                {
                    result = _accountBusiness.FindAccountByKey(user.Id, Key);
                }

                if (result == null) { return NotFound(); }

                return Ok(_mapper.Map<AccountModelDetails>(result));

            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message }); }
            return BadRequest();

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AccountModel model)
        {
            try
            {
                //did at filter
               // if (ModelState.IsValid)
               // {
                    var account = _mapper.Map<Account>(model);
                    var user = await _userManager.FindByNameAsync(User.Identity.Name);
                    account.UserId = user.Id;
                    if (_repository.IsDuplicate(user.Id, account.Name, account.Type, null))
                    {
                        ModelState.AddModelError("IsDuplicate", "It's Duplicate!");
                        return BadRequest(ModelState);
                    }
                    string url = Url.Link("Get", new { key = account.Type.ConvertToCode() + "-" + account.Name });
                    var newAccount = _repository.Insert(account);
                    return Created(url, _mapper.Map<AccountModel>(newAccount));
               // }

               // else
               // {
               //     return BadRequest(ModelState);
               // }

            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message }); }
            return BadRequest();

        }

        [HttpPut("{Key}")]
        public async Task<IActionResult> Put(string Key, [FromBody] AccountModel model)
        {
            try
            {
                //did at filter
               // if (!ModelState.IsValid) { return BadRequest(ModelState); }
               
                //can we add uiserId into Identity so we can have User.Identity.Id instead of User.Identity.Name and use userManager
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                var oldAccount = _accountBusiness.FindAccountByKey(user.Id, Key);

                if (oldAccount == null) { return NotFound(); }

                if (user.Id != oldAccount.UserId) { return Unauthorized(); }


                _mapper.Map(model, oldAccount);
                if (_repository.IsDuplicate(user.Id, oldAccount.Name, oldAccount.Type, oldAccount.Id))
                {
                    ModelState.AddModelError("IsDuplicate", "It's Duplicate!");
                    return BadRequest(ModelState);
                }
                var result = _repository.Upadte(oldAccount.Id, oldAccount);

                if (result == null) { return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "Couldn't Update!" }); }

                return Ok(_mapper.Map<AccountModel>(result));


            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message }); }
            return BadRequest();

        }

        //[HttpPatch]
        //public async Task<IActionResult> Patch(Guid id, [FromBody] Account account)
        //{
        //    try
        //    {
        //        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        //        return Ok();

        //    }
        //    catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message }); }
        //    return BadRequest();

        //}

        [HttpDelete("{Key}")]
        public async Task<IActionResult> Delete(string Key)
        {
            //if has Transaction can't delete
            try
            {

                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var account = _accountBusiness.FindAccountByKey(user.Id, Key);

                if (account == null) { return NotFound(); }

                if (account.UserId != user.Id) { return Unauthorized(); }

                if (_repository.HasTransaction(account.Id)) { return BadRequest(new { message = "Has Transactions, so can not be deleted!" }); }

                var result = _repository.Delete(account.Id);

                if (!result) { return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "couldn't delete." }); }

                return Ok();


            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message }); }
            return BadRequest();


        }

        [HttpGet("{Key}/Transactions")]
        public async Task<IActionResult> GetAllTransactions(string Key)
        {
            try
            {

                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var account = _accountBusiness.FindAccountByKey(user.Id, Key);

                if (account == null) { return BadRequest(); }
                if (account.UserId != user.Id) { return Unauthorized(); }
                var results = _transactionRepository.FindBy(user.Id, t => t.AccountId == account.Id || t.FromAccountId == account.Id);
                /// todo : must mapp transactions to transaction model
               
                return Ok(results);

            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message }); }
            return BadRequest();
        }


        [HttpGet("{Key}/Transactions/{id}")]
        public async Task<IActionResult> GetAllTransactions(string Key, Guid id)//id of transaction
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var account = _accountBusiness.FindAccountByKey(user.Id, Key);
            if (account == null) return BadRequest(); // we don't return notFound becuse its a list
            if (account.UserId != user.Id) { return Unauthorized(); }

            var results = _transactionRepository.FindSingle(user.Id, t => t.AccountId == account.Id && t.Id == id);
            return Ok(results);


          
        }


        //[HttpGet("Test")]
        [HttpOptions("Test")]//if it is an operation api ,and not a resource api
        public IActionResult Test()
        {
            return Ok("Test is Done!");
        }
    }
}