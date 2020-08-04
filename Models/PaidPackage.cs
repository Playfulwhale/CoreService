namespace ApiTemplate.Models
{
    using System.Collections.Generic;
    public class PaidPackage : BaseModel
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Status { get; set; }

        public virtual ICollection<PaidPackagePrice> paidPackagePrices { get; set; }
    }
}
