
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
    [Table("Accounts",Schema ="Expense")]
    public class Account
    {
        [Required]

        public Guid Id { get; set; } //= Guid.NewGuid();

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
        public User User { get; set; }
     
        [Required]
        public AccountType Type { get; set; } = AccountType.Cash;

        [Required]
        public Color Color { get; set; } = Color.Gray;


        [InverseProperty("Account")]
        public ICollection<Transaction> Transactions { get; set; }

        [InverseProperty("ToAccount")]
        public ICollection<Transaction> ToTransactions { get; set; }

        [InverseProperty("FromAccount")]
        public ICollection<Transaction> FromTransactions { get; set; }

        public Account()
        {
            Id = Guid.NewGuid();
            // OpenDate = DateTime.Today;// must get from client becuse today in this computer could be diffrent with client

        }

      //  [Obsolete("Only needed for serialization and materialization", true)]
       
    }
}
