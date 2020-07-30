using System;
using ApiTemplate.Services;
using ApiTemplate.ViewModels.ZoneViewModels;
using Boxed.Mapping;

namespace ApiTemplate.Mappers.ZoneMappers
{
    public class ZoneToSaveZoneMapper : IMapper<Models.Zone, SaveZone>, IMapper<SaveZone, Models.Zone>
    {
        private readonly IClockService _clockService;

        public ZoneToSaveZoneMapper(IClockService clockService) =>
            this._clockService = clockService;

        public void Map(Models.Zone source, SaveZone destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            destination.Title = source.Title;
            destination.Code = source.Code;
            destination.CountryId = source.CountryId;
        }

        public void Map(SaveZone source, Models.Zone destination)
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

            if (destination.CreatedAt == DateTimeOffset.MinValue)
            {
                destination.CreatedAt = now;
            }

            destination.Title = source.Title;
            destination.Code = source.Code;
            destination.CountryId = source.CountryId;
            destination.ModifiedAt = now;
        }
    }
}
