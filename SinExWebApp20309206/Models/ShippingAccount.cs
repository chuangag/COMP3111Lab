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
        
        [Required]
        [StringLength(14,MinimumLength =8)]
        [RegularExpression(@"^[0-9]*$",ErrorMessage ="Phone number must be numeric")]
        public virtual string phoneNumber { get; set; }
        [Required]
        [StringLength(30)]
        [RegularExpression(@"^.*[@].*[.].*$", ErrorMessage = "Please enter a valid email address")]
        public virtual string emailAddress { get; set; }
        
        [StringLength(50)]
        public virtual string buildingInformation { get; set; }
        [Required(ErrorMessage = "The Street field is required")]
        [StringLength(35)]
        public virtual string streetInformation { get; set; }
        [Required(ErrorMessage ="The City field is required")]
        [StringLength(25)]
        public virtual string city { get; set; }
        [Required(ErrorMessage = "The Province field is required")]
        [StringLength(2, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z]$")]
        public virtual string provinceCode { get; set; }
        [StringLength(6, MinimumLength = 5)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Postal code must be numeric")]
        public virtual string postalCode { get; set; }
        [Required(ErrorMessage = "The Type field is required")]
        public virtual string cardType { get; set; }
        [Required]
        [StringLength(19, MinimumLength = 14)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "The field Number must be a number")]
        public virtual string cardNumber { get; set; }
        [Required]
        [StringLength(4, MinimumLength = 3)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "The field Security Number must be a number")]
        public virtual string securityNumber { get; set; }
        [Required(ErrorMessage = "The Cardholder Name field is required")]
        [StringLength(70)]
        public virtual string cardholderName { get; set; }
        [Required]
        [Range(1, 12, ErrorMessage = "Month should between 1-12")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "The field Expiry Month must be a number")]
        public virtual int expiryMonth { get; set; }
        [Required]
        [Range(2017, 2030, ErrorMessage = "Year should between 2017-2030")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "The field Expiry Year must be a number")]
        public virtual int expiryYear { get; set; }

    }
    
}