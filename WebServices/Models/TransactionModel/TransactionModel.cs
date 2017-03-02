using Data.Entities.Expense;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebServices.Models.MappingProfiles;

namespace WebServices.Models.TransactionModel
{
    public class TransactionModel : Model
    {
        [Required]
        public DateTime Date { get; set; } //must get from client not serve = DateTime.Today;

        //2 char for type +date +
        //TR + date + toAc+FromAct:  TR-2017-03-01-CC-AccountName -DB-AccountName

        //ST+date+AccountKey : ST-2017-02-02-CC-AccountName

       //EX+date+AccountKey+CatKey+

        //form Model, base class
        //public string Key { get; set; }//??type+

        [Required]
        public string Type { get; set; } //type.toString()

        public string Color { get; set; }//mapped from type , for transfer :Blue , for Startbalance : Yellow , for Income: green , for expense Red

        [Required]
        public decimal Amount { get; set; }

        //navigation Property
        //for Transactions and startBalance
        public string PayeeName { get; set; }//Name+CategoryKey (Name +(Type+Name+UserName(can have UserName or connot have)))

        public string AccountName { get; set; }
        public string AccountType { get; set; }
        public string AccountKey { get; set; }// Type+Name of Account

        public string CategoryName { get; set; }
        public string CategoryType { get; set; }
        public string CategoryKey { get; set; }//Type+Name+UserName(can have UserName or connot have)

        //For Transfers
        public string ToAccountName { get; set; }
        public string ToAccountType { get; set; }
        public string ToAccountKey { get; set; }// Type+Name of Account


        public string FromAccountName { get; set; }
        public string FromAccountType { get; set; }
        public string FromAccountKey { get; set; }// Type+Name of Account
    }
}
