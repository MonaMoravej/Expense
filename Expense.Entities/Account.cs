using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.Entities
{
    public class Account
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

       // public DateTime OpenDate{get;set;}


       // public double StartBalance { get; set; }

        public Account(Guid id)
        {
            Id = new Guid();
           // OpenDate = DateTime.Today;// must get from client becuse today in this computer could be diffrent with client

        }

        public AccountType AccountType { get; set; }
    }
}
