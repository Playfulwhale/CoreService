namespace ApiTemplate.Mappers.MenuItemMappers
{
    using Boxed.Mapping;
    using System;
    using ViewModels.MenuItemViewModels;


    public class MenuItemDescriptionToMenuItemDescriptionMapper : IMapper<Models.MenuItemDescription, MenuItemDescription>
    {
        public void Map(Models.MenuItemDescription source, MenuItemDescription destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

           
            destination.LanguageCode = source.LanguageCode;
            destination.Title = source.Title;

        }
    }
}
