using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SinExWebApp20309206.ViewModels
{
    public class ShipmentsDateSearchViewModel
    {
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual List<SelectListItem> StartDates { get; set; }
        public virtual List<SelectListItem> EndDates { get; set; }
    }
}