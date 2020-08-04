namespace ApiTemplate.ViewModels.ZoneViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class SaveZone
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public int CountryId { get; set; }
    }
}
