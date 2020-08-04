
using System.Collections.Generic;
namespace ApiTemplate.ViewModels.PaidPackageViewModels
{
    public class PublicPaidPackage
    { 
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<PaidPackagePrice> Prices { get; set; }

    }
}
