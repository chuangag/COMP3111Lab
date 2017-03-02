using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SinExWebApp20309206.ViewModels
{
    public class ShipmentsSearchViewModel
    {
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
       
        public virtual int ShippingAccountId { get; set; }
        public virtual List<SelectListItem> ShippingAccounts { get; set; }
    }
}