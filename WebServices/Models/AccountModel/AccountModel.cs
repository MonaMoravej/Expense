using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebServices.Models.MappingProfiles;


namespace WebServices.Models.AccountModel
{
    public class AccountModel: Model
    {
       
        public string Url { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20,ErrorMessage ="Couldnot be more than 20 charachter!")]
        public string Name { get; set; }

        [Required]
        public decimal StartBalance { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime OpenDate { get; set; }

        [Required]
        public string Type { get; set; } //type.ToString()

        [Required]
        public string Color { get; set; }//Color.ToString()


    }
}
