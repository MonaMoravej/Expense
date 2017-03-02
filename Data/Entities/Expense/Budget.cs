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
    [Table("Budgets", Schema = "Expense")]
    public  class Budget
    {
        public Guid Id { get; set; }

        [Required]
        public decimal Amount { get; set; } 

        [Required]
        public DateTime YearMonth { get; set; }


        //navigation Property
        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; }


        [Required]
        public Guid CategoryId { get; set; }

        public Category Category { get; set; }

        public Budget()
        {
            Id = Guid.NewGuid();
        }
       

    }
}
