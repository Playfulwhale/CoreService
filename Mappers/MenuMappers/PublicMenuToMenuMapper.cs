namespace ApiTemplate.Mappers.MenuMappers
{
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ViewModels.MenuItemViewModels;
    using ViewModels.MenuViewModels;

    public class PublicMenuToMenuMapper : IMapper<Models.Menu, PublicMenu>
    {
        private readonly IMapper<Models.MenuItem, PublicMenuItem> menuItemMapper;
        public PublicMenuToMenuMapper(
            IMapper<Models.MenuItem, PublicMenuItem> menuItemMapper) {
            this.menuItemMapper = menuItemMapper;
        }

        public void Map(Models.Menu source, PublicMenu destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            if(source.menuItems != null)
            {
                var menuItems = menuItemMapper.MapList(source.menuItems);
                var parentMenuItems = menuItems.Where(x => x.ParentId == 0).ToList();

                foreach (var menuItem in parentMenuItems)
                {
                    menuItem.ChildrenItems = GetChildrenItems(menuItem, menuItems);
                }

                destination.menuItems = menuItems;
            }
        }

        public List<PublicMenuItem> GetChildrenItems(PublicMenuItem menuItem, List<PublicMenuItem> menuItems)
        {
            var childrens = menuItems.Where(x => x.ParentId == menuItem.Id).ToList();
            if (menuItem.ChildrenItems == null)
                menuItem.ChildrenItems = new List<PublicMenuItem>();
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
