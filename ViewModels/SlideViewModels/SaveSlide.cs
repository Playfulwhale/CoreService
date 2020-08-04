namespace ApiTemplate.ViewModels.SlideViewModels
{
    using SlideContentViewModels;
    using System.Collections.Generic;

    public class SaveSlide
    {
        public string LinkType { get; set; }

        public string Link { get; set; }

        public string ImageUrl { get; set; }

        public bool OpenNewTab { get; set; }

        public string SortOrder { get; set; }

        public bool Status { get; set; }

        public ICollection<SaveSlideContent>  SlideContents { get; set; }


    }
}
