namespace ApiTemplate.Mappers.PaidPackageMappers
{
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using System;
    using ViewModels.PaidPackageViewModels;

    public class PaidPackageToSavePaidPackageMapper : IMapper<SavePaidPackage, Models.PaidPackage>
    {
        private readonly IClockService _clockService;
        private readonly IMapper<PaidPackagePrice, Models.PaidPackagePrice> _paidPackagePriceMapper;
        public PaidPackageToSavePaidPackageMapper(
            IClockService clockService,
            IMapper<PaidPackagePrice, Models.PaidPackagePrice> paidPackagePriceMapper)
        {
            _paidPackagePriceMapper = paidPackagePriceMapper;
            _clockService = clockService;
        }

        public void Map(SavePaidPackage source, Models.PaidPackage destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
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
            destination.Code = source.Code;
            destination.Name = source.Name;
            destination.Description = source.Description;
            destination.Status = source.Status;
            destination.paidPackagePrices = _paidPackagePriceMapper.MapList(source.paidPackagePrices);
        }

    }
}
