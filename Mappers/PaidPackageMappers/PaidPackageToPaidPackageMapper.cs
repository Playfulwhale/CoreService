namespace ApiTemplate.Mappers.PaidPackageMappers
{
    using Boxed.Mapping;
    using Constants;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using System;
    using ViewModels.PaidPackageViewModels;

    public class PaidPackageToPaidPackageMapper : IMapper<Models.PaidPackage, PaidPackage>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper<Models.PaidPackagePrice, PaidPackagePrice> _paidPackagePriceMapper;

        public PaidPackageToPaidPackageMapper(
           IHttpContextAccessor httpContextAccessor,
           LinkGenerator linkGenerator,
           IMapper<Models.PaidPackagePrice, PaidPackagePrice> paidPackagePriceMapper)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.linkGenerator = linkGenerator;
            _paidPackagePriceMapper = paidPackagePriceMapper;
        }


        public void Map(Models.PaidPackage source, PaidPackage destination)
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
            destination.Status = source.Status;
            destination.paidPackagePrices = _paidPackagePriceMapper.MapList(source.paidPackagePrices);
            //destination.Url = _urlHelper.AbsoluteRouteUrl(PaidPackagesControllerRoute.GetPaidPackage, new { source.Id });
            destination.Url = new Uri(this.linkGenerator.GetUriByRouteValues(
                this.httpContextAccessor.HttpContext,
                PaidPackagesControllerRoute.GetPaidPackage,
                new { source.Id })).ToString();
        }

    }
}
