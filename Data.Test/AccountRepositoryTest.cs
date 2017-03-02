using Data.DbContexts;
using Data.Entities.Expense;
using Data.Entities.Identity;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Test
{
    [TestClass]
    public class AccountRepositoryTest
    {
        
        private readonly Mock<ExpenseDb> _dbMock = new Mock<ExpenseDb>();
        private readonly AccountRepository _repo;

       

        public AccountRepositoryTest()
        {
            _repo = new AccountRepository(_dbMock.Object);
        }

        
        [TestMethod]
        public void AllForInvalidUser()
        {
            var fakeaccount = new Account() { Name="cat2",OpenDate=DateTime.Now,StartBalance=100};
            Mock < DbSet < Account >> acMock = DbSetMock.Create(fakeaccount);
            var results = _repo.All(Guid.Parse("cf15c6c9-f688-47ee-892e-297e530be053"));
            Assert.IsNotNull(results);
          
        }

    }
}
