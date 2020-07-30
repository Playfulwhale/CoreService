namespace ApiTemplate.Models
{
    public class Zone : BaseModel
    { 
        public string Title { get; set; }
        public string Code { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }       
    }
}
