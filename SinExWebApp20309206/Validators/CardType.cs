using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SinExWebApp20309206.Validators
{
    public class CardType:ValidationAttribute
    {
        public CardType():base("only American Express, Diners Club, Discover, MasterCard, UnionPay and Visa are accepted {0}") {

        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null) {
                var valueAsString = value.ToString();
                if (valueAsString != "American Express" && valueAsString != "Diners" && valueAsString != "Club" && valueAsString != "Discover" && valueAsString != "MasterCard" && valueAsString != "UnionPay" && valueAsString != "Visa") {
                    var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(errorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }
}