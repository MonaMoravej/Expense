
using Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.Expense
{
    [Table("Transactions", Schema = "Expense")]
    public class Transaction
    {
        public Guid Id { get; set; }


        [Required]
        public DateTime Date { get; set; } //must get from client not serve = DateTime.Today;


        [Required]
        public TransactionType Type { get; set; } = TransactionType.Expense;

        [Required]
        public decimal Amount { get; set; }

        //navigation Property

        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid? PayeeId { get; set; }

        [ForeignKey("PayeeId")]
        public Payee Payee { get; set; }

        public Guid? AccountId { get; set; }
        
        [ForeignKey("AccountId")]
        public Account Account { get; set; }
        
        public Guid? CategoryId { get; set; }
        
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

       
        public Guid? ToAccountId { get; set; }

        [ForeignKey("ToAccountId")]
        public Account ToAccount { get; set; }
        
          
        public Guid? FromAccountId { get; set; }

        [ForeignKey("FromAccountId")]
        public Account FromAccount { get; set; }

        public Transaction()
        {
            Id = Guid.NewGuid();
        }
    }
}
