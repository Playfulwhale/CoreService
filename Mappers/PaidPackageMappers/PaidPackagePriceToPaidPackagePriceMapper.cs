namespace ApiTemplate.Mappers.PaidPackageMappers
{
    using ViewModels.PaidPackageViewModels;
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using System;

    public class PaidPackagePriceToPaidPackagePriceMapper : IMapper<Models.PaidPackagePrice, PaidPackagePrice>
    {

        public void Map(Models.PaidPackagePrice source, PaidPackagePrice destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }
            
            destination.Currency = source.Currency;
            destination.Price = source.Price;
            destination.Period = source.Period;
        }

    }
}
