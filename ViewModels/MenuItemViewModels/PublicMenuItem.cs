using System.Collections.Generic;

namespace ApiTemplate.ViewModels.MenuItemViewModels
{
    public class PublicMenuItem
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int ParentId { get; set; }

        public List<PublicMenuItem> ChildrenItems { get; set; }

        public string Link { get; set; }
    }
}
 