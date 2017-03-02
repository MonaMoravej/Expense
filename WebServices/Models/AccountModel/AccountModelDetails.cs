using Data.Entities.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServices.Models.MappingProfiles;

namespace WebServices.Models.AccountModel
{
    public class AccountModelDetails:Model
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public decimal StartBalance { get; set; }

        public DateTime OpenDate { get; set; }

        public string Type { get; set; } //type.ToString()

        public string Color { get; set; }//Color.ToString()



        //if its Include==true this list is filled otherwise this list is null , Include =false is default
        public ICollection<TransactionModel.TransactionModel> Transactions { get; set; } //list of transactions that happend with this account
        // could be transfer or expense , it its expense it means transactions
        //if its transfer that means from this acount 
        //and one record for startbalance

        
    }
}
