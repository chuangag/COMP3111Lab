using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SinExWebApp20309206.Models
{
    public class PersonalShippingAccount:ShippingAccount
    {
        [Required(ErrorMessage ="The First Name field is Required")]
        [StringLength(35)]
        public virtual string firstName { get; set; }
        [Required]
        [StringLength(35, ErrorMessage = "The field Last Name must be a string with a maximum length of 35")]
        public virtual string lastName { get; set; }
    }
}