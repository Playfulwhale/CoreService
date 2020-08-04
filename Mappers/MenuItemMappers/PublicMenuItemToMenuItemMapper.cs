namespace ApiTemplate.Mappers.MenuItemMappers
{
    using Boxed.Mapping;
    using System;
    using System.Linq;
    using ViewModels.MenuItemViewModels;
    public class PublicMenuItemToMenuItemMapper : IMapper<Models.MenuItem, PublicMenuItem>
    {
        public void Map(Models.MenuItem source, PublicMenuItem destination)
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
            destination.ParentId = source.ParentId;
            destination.Link = source.Link;
            destination.Title = source.Descriptions.Count() != 0 ? source.Descriptions[0].Title : null;
        }
    }
}
