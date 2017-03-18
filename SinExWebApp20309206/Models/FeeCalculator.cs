using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SinExWebApp20309206.Models
{
    public class FeeCalculator
    {
        public virtual ServicePackageFee ServicePackageFee { get; set; }
        public virtual Currency Currency { get; set; }
    }
}