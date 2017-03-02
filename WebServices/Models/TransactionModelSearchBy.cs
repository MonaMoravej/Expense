using Data.Entities.Expense;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebServices.Models
{
    public class TransactionModelSearchBy
    {
        //[Required]
        public TransactionSortByType SortBy { get; set; } = TransactionSortByType.Date;

        //array of all filters we want to apply
        //if list is null ,all transactions of user will be listed
        public IEnumerable<TransactionSearchByType> SearchBies { get; set; }

        public IEnumerable<DateTime> Dates { get; set; }//just use first , or for range use first and second

       

        //public IEnumerable<string> FromAccounts { }

        //if null, ignore this filter at all
        public IEnumerable<TransactionType> Types { get; set; } //just Transactions or just startBalance ,..



       
    }
}
