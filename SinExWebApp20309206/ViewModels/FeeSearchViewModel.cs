using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SinExWebApp20309206.ViewModels
{
    public class FeeSearchViewModel
    {
        
        public virtual string Origin { get; set; }
        public virtual List<SelectListItem> Origins { get; set; }
        public virtual string Destination { get; set; }
        public virtual List<SelectListItem> Destinations { get; set; }
        public virtual string ServiceType { get; set; }
        public virtual List<SelectListItem> ServiceTypes { get; set; }
        public virtual string PackageType { get; set; }
        public virtual List<SelectListItem> PackageTypes { get; set; }
        public virtual string PackageSize { get; set; }
        public virtual List<SelectListItem> PackageSizes { get; set; }
        public virtual string CurrencyCode { get; set; }
        public virtual List<SelectListItem> CurrencyCodes { get; set; }
        [Required]
        [Range(1, 10, ErrorMessage = "number of package between 1 to 10")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "must be a number")]
        [Display(Name = "Number of Packages(>=1, or calculate as 1)")]
        public virtual int PackageNum { get; set; }
        public virtual bool isOverWeight { get; set; }
    }
}