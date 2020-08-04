namespace ApiTemplate.Models
{
    using System.Collections.Generic;
    public class MenuItem : BaseModel
    {
        public int MenuId { get; set; }

        public int ParentId { get; set; }

        public int Order { get; set; }

        public string Link { get; set; }

        public List<MenuItemDescription> Descriptions { get; set; }
    }
}
