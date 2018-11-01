using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class MinMaxNumberInStock:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var movie = (Movie)validationContext.ObjectInstance;
            var numberInStoke = movie.NumberInStock;
            return (numberInStoke >= 1 && numberInStoke <= 20) 
                ? ValidationResult.Success 
                : new ValidationResult("The feild Number in Stock must be between 1 and 20");
        }
    }
}