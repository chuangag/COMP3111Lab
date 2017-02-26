using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SinExWebApp20309206.Models
{
    [Table("ServiceType")]
    public class ServiceType
    {
        public virtual int ServiceTypeID { get; set; }
        public virtual  string Type { get; set; }
        public virtual string CutoffTime { get; set; }
        public virtual string DeliveryTime { get; set; }
        public virtual ICollection<ServicePackageFee> ServicePackageFees { get; set; }
    }
}