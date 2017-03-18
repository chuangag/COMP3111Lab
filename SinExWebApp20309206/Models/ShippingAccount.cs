using SinExWebApp20309206.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SinExWebApp20309206.Models
{
    [Table("ShippingAccount")]
    public abstract class ShippingAccount
    {
        
        
        public virtual int ShippingAccountId { get; set; }
        
        [StringLength(10,MinimumLength =6)]
        public virtual string UserName { get; set; }
        [Required]
        [StringLength(14,MinimumLength =8)]
        [RegularExpression(@"^[0-9]*$",ErrorMessage ="Phone number must be numeric")]
        [Display(Name ="Phonenumber")]
        public virtual string phoneNumber { get; set; }
        [Required]
        [StringLength(30)]
        [RegularExpression(@"^.*[@].*[.].*$", ErrorMessage = "Please enter a valid email address")]
        [Display(Name = "Email")]
        public virtual string emailAddress { get; set; }
        [Required(ErrorMessage = "The Building field is required")]
        [StringLength(50)]
        [Display(Name = "Building")]
        public virtual string buildingInformation { get; set; }
        [Required(ErrorMessage = "The Street field is required")]
        [StringLength(35)]
        [Display(Name = "Street")]
        public virtual string streetInformation { get; set; }
        [Required(ErrorMessage ="The City field is required")]
        [StringLength(25)]
        [Display(Name = "City")]
        public virtual string city { get; set; }
        [Required(ErrorMessage = "The Province field is required")]
        [StringLength(2, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z]*$",ErrorMessage ="Please input a valid province code")]
        [Display(Name = "Province")]
        public virtual string provinceCode { get; set; }
        [StringLength(6, MinimumLength = 5)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Postal code must be numeric")]
        [Display(Name = "Postal Code")]
        public virtual string postalCode { get; set; }
        [Required(ErrorMessage = "The Type field is required")]
        [Display(Name = "Type")]
        [RegularExpression(@"^American Express$|^Diners Club$|^Discover$|^MasterCard$|^UnionPay$|^Visa$", ErrorMessage = "only American Express, Diners Club, Discover, MasterCard, UnionPay and Visa are accepted")]
        public virtual string cardType { get; set; }
        [Required(ErrorMessage = "The Number field is required")]
        [StringLength(19, MinimumLength = 14)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "The field Number must be a number")]
        [Display(Name = "Number")]
        public virtual string cardNumber { get; set; }
        [Required]
        [StringLength(4, MinimumLength = 3)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "The field Security Number must be a number")]
        [Display(Name = "Security Number")]
        public virtual string securityNumber { get; set; }
        [Required(ErrorMessage = "The Cardholder Name field is required")]
        [StringLength(70)]
        [Display(Name = "Cardholder Name")]
        public virtual string cardholderName { get; set; }
        [Required]
        [Range(1, 12, ErrorMessage = "Month should between 1-12")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "The field Expiry Month must be a number")]
        [Display(Name = "Expiry Month")]
        public virtual int expiryMonth { get; set; }
        [Required]
        [Range(2017, 2030, ErrorMessage = "Year should between 2017-2030")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "The field Expiry Year must be a number")]
        [Display(Name = "Expiry Year")]
        public virtual int expiryYear { get; set; }

        public virtual ICollection<Shipment> Shipments { get; set; }
        /*public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
            if (cardType != "American Express" && cardType != "Diners" && cardType != "Club" && cardType != "Discover" && cardType != "MasterCard" && cardType != "UnionPay" && cardType != "Visa") {
                yield return new ValidationResult("only American Express, Diners Club, Discover, MasterCard, UnionPay and Visa are accepted", new[] { "Type" });
            }
        }*/

    }
    
}