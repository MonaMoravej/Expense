using Expense.Entities.Expense;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Expense.Data.Entities.Identity
{
    [Table("Language", Schema = "Identity")]
    public class Language
    {
        [Required]
        [Key]
        public Guid Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }


       
    }
}