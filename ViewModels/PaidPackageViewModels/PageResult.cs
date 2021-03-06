﻿namespace ApiTemplate.ViewModels.PaidPackageViewModels
{
    using System.Collections.Generic;

    public class PageResult<T>
        where T : class
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<PaidPackagePrice> paidPackagePrices { get; set; }


    }
}
