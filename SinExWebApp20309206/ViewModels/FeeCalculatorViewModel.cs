using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using X.PagedList;

namespace SinExWebApp20309206.ViewModels
{
    public class FeeCalculatorViewModel
    {
        
        public virtual FeeSearchViewModel ServicePackageFee { get; set; }
        public virtual IEnumerable<FeeListViewModel> ServicePackageFees { get; set; }
        public virtual FeeResultViewModel FeeResult { get; set; }
    }
}