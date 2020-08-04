
namespace ApiTemplate.Mappers.MenuItemMappers
{
    using Boxed.Mapping;
    using Constants;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using System;
    using ViewModels.MenuItemViewModels;

    public class MenuItemToMenuItemMapper : IMapper<Models.MenuItem, MenuItem>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly LinkGenerator linkGenerator;
        IMapper<Models.MenuItemDescription, MenuItemDescription> menuItemDescriptionMapper;

        public MenuItemToMenuItemMapper(
           IHttpContextAccessor httpContextAccessor,
           LinkGenerator linkGenerator,
           IMapper<Models.MenuItemDescription, MenuItemDescription> menuItemDescriptionMapper)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.linkGenerator = linkGenerator;
            this.menuItemDescriptionMapper = menuItemDescriptionMapper;
        }

        public void Map(Models.MenuItem source, MenuItem destination)
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
            destination.MenuId = source.MenuId;
            destination.ParentId = source.ParentId;
            destination.Order = source.Order;
            destination.Link = source.Link;
            destination.Active = source.Active;
            destination.Descriptions = menuItemDescriptionMapper.MapList(source.Descriptions);
            //destination.Url = urlHelper.AbsoluteRouteUrl(MenuItemsControllerRoute.GetMenuItem, new { source.Id });
            destination.Url = new Uri(this.linkGenerator.GetUriByRouteValues(
                this.httpContextAccessor.HttpContext,
                MenuItemsControllerRoute.GetMenuItem,
                new { source.Id })).ToString();
        }
    }
}
