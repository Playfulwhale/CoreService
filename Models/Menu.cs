namespace ApiTemplate.Models
{
    using System.Collections.Generic;
    public class Menu : BaseModel
    {
        public string Name { get; set; }

        public string Position { get; set; }

        public List<MenuItem> menuItems { get; set; }
    }
}
