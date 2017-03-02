using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServices.Models
{
    public class SerachedTransactions
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; } 

        public string TypeName { get; set; }

        public decimal Amount { get; set; }
        public string PayeeName { get; set; }

        public string AccountName { get; set; }

        public string CategoryName { get; set; }

        public string ToAccountName { get; set; }

        public string FromAccountName { get; set; }


    }
}
