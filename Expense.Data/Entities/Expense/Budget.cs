using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.Entities.Expense
{
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


        [Required]
        public Guid CategoryId { get; set; }

        public Category Category { get; set; }

        public Budget()
        {
            Id = Guid.NewGuid();
        }
    }
}
