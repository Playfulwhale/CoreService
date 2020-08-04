namespace ApiTemplate.ViewModels.MenuItemViewModels
{
    using Swashbuckle.AspNetCore.Annotations;
    using System.Collections.Generic;
    public class MenuItem
    {
        public int Id { get; set; }

        public int MenuId { get; set; }

        public int ParentId { get; set; }

        public int Order { get; set; }

        public string Link { get; set; }

        public bool Active { get; set; }
       
        public List<MenuItemDescription> Descriptions { get; set; }

        public string Url { get; set; }
    }
}
 