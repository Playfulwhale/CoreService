using System;
using ApiTemplate.Services;
using ApiTemplate.ViewModels.MenuItemViewModels;
using Boxed.Mapping;

namespace ApiTemplate.Mappers.MenuItemMappers
{
    public class MenuItemDescriptionToSaveMenuItemDescriptionMapper : IMapper<SaveMenuItemDescription, Models.MenuItemDescription>
    {
        private readonly IClockService clockService;

        public MenuItemDescriptionToSaveMenuItemDescriptionMapper(IClockService clockService) =>
            this.clockService = clockService;

        public void Map(SaveMenuItemDescription source, Models.MenuItemDescription destination)
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

            if (destination.CreatedAt == null)
            {
                destination.CreatedAt = now;
            }

            destination.Title = source.Title;
            destination.LanguageCode = source.LanguageCode;
            destination.ModifiedAt = now;
        }
    }
}
