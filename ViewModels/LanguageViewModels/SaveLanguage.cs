namespace ApiTemplate.ViewModels.LanguageViewModels
{
    using System.ComponentModel.DataAnnotations;
    public class SaveLanguage
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Flag { get; set; }

        public bool? IsDefault { get; set; }

        public bool Active { get; set; }
    }
}
