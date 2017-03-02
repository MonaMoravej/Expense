using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebServices.Models.TransactionModel;
using Data.Entities.Expense;
using Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebServices.Controllers;

namespace WebServices.Models.MappingProfiles
{
    //for AccountUrl works
    public class GenericUrlResolver<TSource, TDestination> : IValueResolver<TSource, TDestination, string> where TDestination : Model
    {
        private IHttpContextAccessor _httpContextAccessor;
        public GenericUrlResolver(IHttpContextAccessor ctx)
        {
            _httpContextAccessor = ctx;
        }

        public string Resolve(TSource source, TDestination destination, string destMember, ResolutionContext context)
        {
            var url = (IUrlHelper)_httpContextAccessor.HttpContext.Items[BaseController.URLHELPER];
            return url.Link("Get", new { Key = destination.Key });
        }
    }


    //AccountName and ToAccountName and FromAccountName
    public class AccountNameResolver : IValueResolver<Transaction, TransactionModel.TransactionModel, string>
    {
        
        public string Resolve(Transaction source, TransactionModel.TransactionModel destination, string destMember, ResolutionContext context)
        {
            if (source.Account != null)
                return source.Account.Name;
            return string.Empty;
        }
    }
}
