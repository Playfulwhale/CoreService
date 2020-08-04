namespace ApiTemplate.Mappers.MenuItemMappers
{
    using ViewModels.MenuItemViewModels;
    using Boxed.Mapping;
    using Services;
    using System;

    public class MenuItemToSaveMenuItemMapper : IMapper<SaveMenuItem, Models.MenuItem>
    {
        private readonly IClockService clockService;
        private readonly IMapper<SaveMenuItemDescription, Models.MenuItemDescription> _menuItemDescriptionMapper;

        public MenuItemToSaveMenuItemMapper(IClockService clockService,
             IMapper<SaveMenuItemDescription, Models.MenuItemDescription> menuItemDescriptionMapper)
        {
            this.clockService = clockService;
            _menuItemDescriptionMapper = menuItemDescriptionMapper;
        }
            

        public void Map(SaveMenuItem source, Models.MenuItem destination)
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

            destination.ModifiedAt = now;
            destination.MenuId = source.MenuId;
            destination.ParentId = source.ParentId;
            destination.Order = source.Order;
            destination.Link = source.Link;
            destination.Active = source.Active;
            destination.Descriptions = _menuItemDescriptionMapper.MapList(source.Descriptions);
        }
    }
}
