namespace ApiTemplate.Mappers.CountryMappers
{
    using Boxed.AspNetCore;
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using ViewModels.CountryViewModels;
    using Constants;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    public class CountryToCountryMapper : IMapper<Models.Country, Country>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly LinkGenerator linkGenerator;

        public CountryToCountryMapper(
           IHttpContextAccessor httpContextAccessor,
           LinkGenerator linkGenerator)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.linkGenerator = linkGenerator;
        }

        public void Map(Models.Country source, Country destination)
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
            destination.Name = source.Name;
            destination.IsoCode2 = source.IsoCode2;
            destination.IsoCode3 = source.IsoCode3;
            destination.AddressFormat = source.AddressFormat;
            destination.Active = source.Active;
            destination.PostcodeRequired = source.PostcodeRequired;            
            destination.Default = source.Default;
            //destination.Url = urlHelper.AbsoluteRouteUrl(CountriesControllerRoute.GetCountry,new {countryId = source.Id});     
            destination.Url = new Uri(this.linkGenerator.GetUriByRouteValues(
                this.httpContextAccessor.HttpContext,
                CountriesControllerRoute.GetCountry,
                new { source.Id })).ToString();
        }   
    }
}
