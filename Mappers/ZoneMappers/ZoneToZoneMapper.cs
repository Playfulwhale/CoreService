using System;
using ApiTemplate.Constants;
using ApiTemplate.ViewModels.ZoneViewModels;
using Boxed.AspNetCore;
using Boxed.Mapping;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ApiTemplate.Mappers.ZoneMappers
{
    public class ZoneToZoneMapper : IMapper<Models.Zone, Zone>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly LinkGenerator linkGenerator;

        public ZoneToZoneMapper(
           IHttpContextAccessor httpContextAccessor,
           LinkGenerator linkGenerator)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.linkGenerator = linkGenerator;
        }
        public void Map(Models.Zone source, Zone destination)
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
            destination.CountryId = source.CountryId;
            //destination.Url = _urlHelper.AbsoluteRouteUrl(ZonesControllerRoute.GetZone, new { zoneId = source.Id });
            destination.Url = new Uri(this.linkGenerator.GetUriByRouteValues(
                this.httpContextAccessor.HttpContext,
                ZonesControllerRoute.GetZone,
                new { zoneId = source.Id })).ToString();
        }
    }
}
