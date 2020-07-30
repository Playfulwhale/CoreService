namespace ApiTemplate.Mappers.ZoneMappers
{
    using Boxed.AspNetCore;
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using ViewModels.ZoneViewModels;
    using Constants;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.AspNetCore.Http;

    public class PublicZoneToZoneMapper : IMapper<Models.Zone, PublicZone>
    {
        public void Map(Models.Zone source, PublicZone destination)
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
            destination.Title = source.Title;
            destination.Code = source.Code;
            //destination.CountryId = source.CountryId;
            // destination.Url = _urlHelper.AbsoluteRouteUrl(ZonesControllerRoute.GetZone, new { zoneId = source.Id });
            //destination.Url = new Uri(this.linkGenerator.GetUriByRouteValues(
            //    this.httpContextAccessor.HttpContext,
            //    ZonesControllerRoute.GetZone,
            //    new { source.Id })).ToString();
        }
    }
}
