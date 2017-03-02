using AutoMapper;
using Data.Entities.Identity;
using Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServices.Businesses;

namespace WebServices.Controllers
{
    [Authorize]
    //just for update / Insert / Delete /all with 
    //for read we must have api/Accounts/{key}/Transactions
    //api/Payees/{key}/Transactions
    //api/Category/{key}/transactions

    //[Route("api/Accounts/{AccountKey}/Transactions")]
    public class TransactionsController:BaseController
    {
        private ITransactionRepository _repository;
        private UserManager<User> _userManager;
        private IMapper _mapper;
        private AccountBusiness _accountBusiness;

        public TransactionsController(ITransactionRepository repository , UserManager<User> userManager, IMapper mapper, AccountBusiness accountBusiness)
        {
            _repository = repository;
            _userManager = userManager;
            _mapper = mapper;
            _accountBusiness = accountBusiness;
        }

        //Get Alls:
        //1.by userId
        //2.By UserId and AccountId
        //3.By UserId and PayeeId
        //
        //[HttpGet("api/Transactions")]
        //public async Task<IActionResult> Get()
        //{
        //    var user = await _userManager.FindByNameAsync(User.Identity.Name);
        //   var results= _repository.All(user.Id);
        //    return Ok(results);
        //}

       //move to account controller
        //[Route("api/Accounts/{AccountKey}/Transactions")]
        //[HttpGet]
        //public async Task<IActionResult> GetByAccount(string AccountKey)
        //{
        //    var user = await _userManager.FindByNameAsync(User.Identity.Name);
        //    var account = _accountBusiness.FindAccountByKey(user.Id, AccountKey);
        //    if (account == null) return Ok(null); // we don't return notFound becuse its a list

        //    var results = _repository.FindBy(user.Id, t => t.AccountId == account.Id);
        //    return Ok(results);
        //}


        
       [Route("api/Accounts/{AccountKey}/Transactions/{id}")]
       [HttpGet]
        public async Task<IActionResult> GetByAccount(string AccountKey,Guid id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            return Ok();
        }
    }
}
