namespace ApiTemplate.ViewModels.MenuViewModels
{
    using ViewModels.MenuItemViewModels;
    using System.Collections.Generic;
    public class AdminMenu
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Position { get; set; }

        public List<AdminMenuItem> menuItems { get; set; }
    }
}
