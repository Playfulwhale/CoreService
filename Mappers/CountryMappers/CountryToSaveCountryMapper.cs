using System;
using ApiTemplate.Services;
using ApiTemplate.ViewModels.CountryViewModels;
using Boxed.Mapping;

namespace ApiTemplate.Mappers.CountryMappers
{
    public class CountryToSaveCountryMapper : IMapper<Models.Country, SaveCountry>, IMapper<SaveCountry, Models.Country>
    {
        private readonly IClockService clockService;

        public CountryToSaveCountryMapper(IClockService clockService) =>
            this.clockService = clockService;

        public void Map(Models.Country source, SaveCountry destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }
           
            destination.Name = source.Name;
            destination.IsoCode2 = source.IsoCode2;
            destination.IsoCode3 = source.IsoCode3;
            destination.AddressFormat = source.AddressFormat;
            destination.PostcodeRequired = source.PostcodeRequired;          
        }

        public void Map(SaveCountry source, Models.Country destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            var now = clockService.UtcNow;

            if (destination.CreatedAt == DateTimeOffset.MinValue)
            {
                destination.CreatedAt = now;
            }

            destination.Name = source.Name;
            destination.IsoCode2 = source.IsoCode2;
            destination.IsoCode3 = source.IsoCode3;
            destination.AddressFormat = source.AddressFormat;
            destination.PostcodeRequired = source.PostcodeRequired;                       
            destination.ModifiedAt = now;
        }
    }
}
