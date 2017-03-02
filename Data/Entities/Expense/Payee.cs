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
    [Table("Payees", Schema = "Expense")]
    public  class Payee
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Column(TypeName ="nvarchar(max)")]
        public string Memo { get; set; }


        //navigation Property 

        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }


        public ICollection<Transaction> Transactions { get; set; }

        public Payee()
        {
            Id = Guid.NewGuid();
        }

    }
}
