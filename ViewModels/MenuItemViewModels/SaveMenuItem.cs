namespace ApiTemplate.ViewModels.MenuItemViewModels
{
    using System.Collections.Generic;
    public class SaveMenuItem
    {
        public int MenuId { get; set; }

        public int ParentId { get; set; }

        public int Order { get; set; }

        public string Link { get; set; }

        public bool Active { get; set; }

        public List<SaveMenuItemDescription> Descriptions { get; set; }
    }
}
