namespace ApiTemplate.Models
{
    public class SlideContent : BaseModel
    {
        public int SlideId { get; set; }
        public Slide Slide { get; set; }

        public string Title { get; set; }

        public string Caption { get; set; }

        public string LanguageCode { get; set; }
    }
}
