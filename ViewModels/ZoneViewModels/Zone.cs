namespace ApiTemplate.ViewModels.ZoneViewModels
{
    using Swashbuckle.AspNetCore.Annotations;

    public class Zone
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int CountryId { get; set; }
        public string Url { get; set; }
    }
}
