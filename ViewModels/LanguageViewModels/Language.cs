namespace ApiTemplate.ViewModels.LanguageViewModels
{
    public class Language
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Code { get; set; }

        public string Flag { get; set; }

        public bool? IsDefault { get; set; }

        public bool Active { get; set; }

        public string Url { get; set; }
    }
}
