using Expense.Data.Entities.Identity;
using Expense.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.Entities.Expense
{
    public class Account
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public decimal StartBalance { get; set; }

        [Required]
        public DateTime OpenDate { get; set; }

        //navigation property
        
         [Required]
         public Guid UserId { get; set; }

        [Required]
        public AccountType AccountType { get; set; } = AccountType.Cash;

        [Required]
        public Color Color { get; set; } = Color.Gray;


        [InverseProperty("Account")]
        public ICollection<Transaction> Transactions { get; set; }

        [InverseProperty("ToAccount")]
        public ICollection<Transaction> ToTransactions { get; set; }

        [InverseProperty("FromAccount")]
        public ICollection<Transaction> FromTransactions { get; set; }

        public Account(Guid id)
        {
            Id = new Guid();
           // OpenDate = DateTime.Today;// must get from client becuse today in this computer could be diffrent with client

        }

       
    }
}
