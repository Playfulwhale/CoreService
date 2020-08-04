namespace ApiTemplate.Mappers.MenuMappers
{
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ViewModels.MenuItemViewModels;
    using ViewModels.MenuViewModels;

    public class AdminMenuToMenuMapper : IMapper<Models.Menu, AdminMenu>
    {
        private readonly IMapper<Models.MenuItem, AdminMenuItem> menuItemMapper;
        public AdminMenuToMenuMapper(
            IMapper<Models.MenuItem, AdminMenuItem> menuItemMapper)
        {
            this.menuItemMapper = menuItemMapper;
        }

        public void Map(Models.Menu source, AdminMenu destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            var menuItems = menuItemMapper.MapList(source.menuItems);
            var parentMenuItems = menuItems.Where(x => x.ParentId == 0).ToList();

            foreach (var menuItem in parentMenuItems)
            {
                menuItem.ChildrenItems = GetChildrenItems(menuItem, menuItems);
            }
            destination.Id = source.Id;
            destination.Name = source.Name;
            destination.Position = source.Position;
            destination.menuItems = menuItems;
        }

        public List<AdminMenuItem> GetChildrenItems(AdminMenuItem menuItem, List<AdminMenuItem> menuItems)
        {
            var childrens = menuItems.Where(x => x.ParentId == menuItem.Id).ToList();
            if (menuItem.ChildrenItems == null)
                menuItem.ChildrenItems = new List<AdminMenuItem>();
            menuItem.ChildrenItems.AddRange(childrens);
            foreach (var item in childrens)
            {
                menuItems.Remove(item);
                GetChildrenItems(item, menuItems);
            }

            return menuItem.ChildrenItems;

        }
    }
}
