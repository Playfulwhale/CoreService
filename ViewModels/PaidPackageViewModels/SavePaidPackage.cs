namespace ApiTemplate.ViewModels.PaidPackageViewModels
{
    using System.Collections.Generic;
    public class SavePaidPackage
    {
        
        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Status { get; set; }

        public ICollection<PaidPackagePrice> paidPackagePrices { get; set; }
    }
}
