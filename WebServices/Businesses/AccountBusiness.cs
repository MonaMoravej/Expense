using Data.Entities.Expense;
using Data.Entities.Identity;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServices.Businesses
{
    public class AccountBusiness
    {
        private IAccountRepository _repository;
     
        public AccountBusiness(IAccountRepository repository )
        {
            _repository = repository;
            
        }
        public  Account FindAccountByKey(Guid userId, string Key)
        {
            var name = Key.Split('-')[1];


            var type = (Key.Split('-')[0]).ConvertToAccountType();
           

            return _repository.FindSingle(userId, a => a.Name == name && a.Type == type);

        }

        public Account FindAccountByKeyIncludeTransactions(Guid userId, string Key)
        {
            var name = Key.Split('-')[1];
            var type = (Key.Split('-')[0]).ConvertToAccountType();
          

            return _repository.FindSingleInclude(userId, a => a.Name == name && a.Type == type);

        }
    }
}
