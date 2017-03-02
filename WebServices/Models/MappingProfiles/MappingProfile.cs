using AutoMapper;
using Data.Entities.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServices.Models.MappingProfiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountModel.AccountModel>().BeforeMap((a,am)=>am.Key=a.Type.ConvertToCode()+"-"+a.Name)
               // .ForMember(an => an.TestMapping, opt => opt.MapFrom(ao => ao.Color))
               // .ForMember(an => an.OpenDate, opt => opt.ResolveUsing(ao => ao.OpenDate.Date.ToString()))
               // .ForMember(an => an.Key, opt => opt.ResolveUsing(ao => ao.Type.ToString() + "-" + ao.Name))
                //.ForMember(an => an.Url, opt => opt.MapFrom(ao => $"/api/Accounts/{ao.Id}")); 
                .ForMember(am => am.Url, opt => opt.ResolveUsing<GenericUrlResolver<Account,AccountModel.AccountModel>>())
                .ReverseMap()
                .ForMember(a=>a.Type,opt=>opt.ResolveUsing(am=> (AccountType)Enum.Parse(typeof(AccountType),am.Type)))
                .ForMember(a => a.Color, opt => opt.ResolveUsing(am => (Color)Enum.Parse(typeof(Color), am.Color)));
            
            CreateMap<Account,AccountModel.AccountModelDetails>().BeforeMap((a, am) => am.Key = a.Type.ConvertToCode() + "-" + a.Name)
                 .ForMember(an => an.Url, opt => opt.ResolveUsing<GenericUrlResolver<Account, AccountModel.AccountModelDetails>>());

            CreateMap<Transaction, TransactionModel.TransactionModel>()
                .ForMember(tm=>tm.AccountName,opt=>opt.ResolveUsing<AccountNameResolver>())
                .ForMember(tm=>tm.Color,opt=>opt.ResolveUsing((t, tm) =>
                {
                    if (t.Type == TransactionType.Expense) return "Red";
                    else return "Green";

                } ))
                .ReverseMap();
        }

    }
}
