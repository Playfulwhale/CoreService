namespace ApiTemplate.Models
{
    using System.Collections.Generic;
    public class Slide : BaseModel
    {
        public string LinkType { get; set; }

        public string Link { get; set; }

        public string ImageUrl { get; set; }

        public bool OpenNewTab { get; set; }

        public string SortOrder { get; set; }

        public bool Status { get; set; }

        public ICollection<SlideContent> SlideContents { get; set; }
    }
}
