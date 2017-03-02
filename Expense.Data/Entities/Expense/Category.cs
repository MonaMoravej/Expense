using Expense.Data.Entities.Expense;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.Entities.Expense
{
    public class Category
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public CategoryType Type { get; set; } = CategoryType.Expense;


        //navigation property
        public Guid? UserId { get; set; }


        public Guid? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public Category Parent { get; set; }

        [InverseProperty("Parent")]
        public ICollection<Category> Children { get; set; }

        public Guid? IconId { get; set; }

        [ForeignKey("IconId")]
        public Icon Icon { get; set; }


        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<Payee> Payees { get; set; }
        public ICollection<Budget> Budgets { get; set; }

        public Category()
        {
            Id = Guid.NewGuid();
        }
    }
}
