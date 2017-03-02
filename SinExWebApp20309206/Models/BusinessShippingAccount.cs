using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SinExWebApp20309206.Models
{
    public class BusinessShippingAccount:ShippingAccount
    {
        [Required]
        [StringLength(70)]
        [Display(Name ="Contact Person Name")]
        public virtual string contactPersonName { get; set; }
        [Required]
        [StringLength(40)]
        [Display(Name = "Company Name")]
        public virtual string companyName { get; set; }
        [StringLength(30)]
        [Display(Name = "Department Name")]
        public virtual string departmentName { get; set; }
    }
}