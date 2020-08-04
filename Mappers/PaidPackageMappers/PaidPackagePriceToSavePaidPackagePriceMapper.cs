namespace ApiTemplate.Mappers.PaidPackageMappers
{
    using Services;
    using ViewModels.PaidPackageViewModels;
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using System;

    public class PaidPackagePriceToSavePaidPackagePriceMapper : IMapper<PaidPackagePrice, Models.PaidPackagePrice>
    {
        private readonly IClockService _clockService;
        public PaidPackagePriceToSavePaidPackagePriceMapper(
            IClockService clockService)
        {
            _clockService = clockService;
        }

        public void Map(PaidPackagePrice source, Models.PaidPackagePrice destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }
            var now = _clockService.UtcNow;

            if (destination.CreatedAt == null)
            {
                destination.CreatedAt = now;
            }
            destination.ModifiedAt = now;
            destination.Currency = source.Currency;
            destination.Price = source.Price;
            destination.Period = source.Period;
        }

    }
}
