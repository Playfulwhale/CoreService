namespace ApiTemplate.Mappers.CountryMappers
{
    using Boxed.AspNetCore;
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using ViewModels.CountryViewModels;
    using Constants;

    public class PublicCountryToCountryMapper : IMapper<Models.Country, PublicCountry>
    {

        public void Map(Models.Country source, PublicCountry destination)
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
           // destination.AddressFormat = source.AddressFormat;
            destination.PostcodeRequired = source.PostcodeRequired;
        }
    }
}
