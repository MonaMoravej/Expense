using Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebServices.Models.UserModel
{
    public class UserProfile
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

       
        public string PhoneNumber { get; set; }

        public DateTime? BirthDate { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public HumanGender Gender { get; set; }


        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        public byte[] Picture { get; set; }

        [Required]
        public string CurrencyName { get; set; }

        [Required]
        public string LanguageName { get; set; }

    }
}
