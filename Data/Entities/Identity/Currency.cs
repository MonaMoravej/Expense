using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace Data.Entities.Identity
{
    [Table("Currency", Schema = "Identity")]
    
    public class Currency
    {
        [Required]
        [Key]
        public Guid Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }
    }
    //can add format for avoid .00 for IRR/Iran
}