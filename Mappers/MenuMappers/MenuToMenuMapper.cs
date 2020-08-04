namespace ApiTemplate.Mappers.MenuMappers
{
    using Boxed.Mapping;
    using Constants;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using System;
    using ViewModels.MenuItemViewModels;
    using ViewModels.MenuViewModels;

    public class MenuToMenuMapper : IMapper<Models.Menu, Menu>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper<Models.MenuItem, MenuItem> menuItemMapper;

        public MenuToMenuMapper(
           IHttpContextAccessor httpContextAccessor,
           LinkGenerator linkGenerator,
           IMapper<Models.MenuItem, MenuItem> menuItemMapper)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.linkGenerator = linkGenerator;
            this.menuItemMapper = menuItemMapper;
        }

        public void Map(Models.Menu source, Menu destination)
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
            destination.Position = source.Position;
            //destination.Url = urlHelper.AbsoluteRouteUrl(MenusControllerRoute.GetMenu, new { source.Id });
            destination.Url = new Uri(this.linkGenerator.GetUriByRouteValues(
                    this.httpContextAccessor.HttpContext,
                    MenusControllerRoute.GetMenu,
                    new { source.Id })).ToString();
        }

    }
}
