namespace ApiTemplate.Models
{
    public class Country : BaseModel
    {   
        public Country()
        {
            Default = false;
        }
        public string Name { get; set; }      
        public string IsoCode2 { get; set; }       
        public string IsoCode3 { get; set; }     
        public string AddressFormat { get; set; }      
        public bool? PostcodeRequired { get; set; }            
        public bool? Default { get; set; }
    }
}
