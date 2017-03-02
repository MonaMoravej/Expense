using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.Entities.Expense
{
  public  class Icon
    {

        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        public byte[] Image { get; set; }

        //navigation Property
        public ICollection<Category> Categories { get; set; }

        public Icon()
        {
            Id = Guid.NewGuid();
        }

        //Car,
        //CarService,
        //Bank,
        //Cash,
        //Charity,
        //Children,
        //Clothing,
        //CreditCard,
        //Eating,
        //Education,
        //Entertainment,
        //Gifts,
        //Shopping,
        //Fitness,
        //Home,
        //HomeRepair,
        //HouseHold,
        //Insurance,
        //Loan,
        //Payment,
        //Pets,
        //Tax,
        //Transport,
        //Travel,
        //Utilites,
        //TV,
        //Garbage,
        //Gas,
        //Internet,
        //Telephone,
        //Water,
        //Bonus,
        //Salary,
        //Saving,
        //RedOthers,
        //GreenOthers,
        //YellowOthers,

        //Accounts,
        //Budget,
        //Summery,
        //Report,
        //Category,
        //BarChart,
        //LinerChart,
        //Bill,
        //Refresh,
        //Settings

    }
}
