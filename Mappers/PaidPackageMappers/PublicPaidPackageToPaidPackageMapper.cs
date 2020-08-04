namespace ApiTemplate.Mappers.PaidPackageMappers
{
    using ViewModels.PaidPackageViewModels;
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using System;

    public class PublicPaidPackageToPaidPackageMapper : IMapper<Models.PaidPackage, PublicPaidPackage>
    {
        private readonly IMapper<Models.PaidPackagePrice, PaidPackagePrice> _paidPackagePriceMapper;
        public PublicPaidPackageToPaidPackageMapper(
            IMapper<Models.PaidPackagePrice, PaidPackagePrice> paidPackagePriceMapper)
        {
            _paidPackagePriceMapper = paidPackagePriceMapper;
        }

        public void Map(Models.PaidPackage source, PublicPaidPackage destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            destination.Id = source.Id;
            destination.Code = source.Code;
            destination.Name = source.Name;
            destination.Description = source.Description;
            destination.Prices = _paidPackagePriceMapper.MapList(source.paidPackagePrices);
        }

    }
}
