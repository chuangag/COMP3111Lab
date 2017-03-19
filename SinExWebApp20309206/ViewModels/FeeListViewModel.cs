namespace SinExWebApp20309206.ViewModels
{
    public class FeeListViewModel
    {

        public virtual string Origin { get; set; }
        public virtual string Destination { get; set; }
        public virtual string ServiceType { get; set; }
        public virtual string PackageType { get; set; }
        public virtual int Weight { get; set; }
        public virtual float Cost { get; set; }
       
        
    }
}