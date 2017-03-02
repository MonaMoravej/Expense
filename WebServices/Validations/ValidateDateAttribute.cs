using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebServices.Validations
{
    public class ValidateDateAttribute:ValidationAttribute //,IClientValidation because it's a webApi
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value != null)
            {
                DateTime _birthJoin = Convert.ToDateTime(value);
                if (_birthJoin > DateTime.Now)
                {
                    return new ValidationResult("Birth date can not be greater than current date.");
                }
            
        }
            return ValidationResult.Success;
           // return base.IsValid(value, validationContext);
        }
    }
}
