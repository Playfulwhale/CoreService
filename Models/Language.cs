namespace ApiTemplate.Models
{
    public class Language : BaseModel
    {
        public string Title { get; set; }

        public string Code { get; set; }

        public string Flag { get; set; }

        public bool? IsDefault { get; set; }
    }
}
