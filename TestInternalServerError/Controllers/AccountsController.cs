using Data.Entities.Expense;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestInternalServerError.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController: Controller
    {
        private IRepository<Account> _repository;

        [HttpGet]
        public IActionResult Get()
        {
            var userId = Guid.Parse("cf15c6c9-f688-47ee-892e-297e530be053");

          

            return Ok(_repository.All(userId));
        }

        public AccountsController(IRepository<Account> ra)
        {
            _repository = ra;
        }

    }
}
