namespace ApiTemplate.ViewModels.CountryViewModels
{
    using Swashbuckle.AspNetCore.Annotations;

    public class PublicCountry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IsoCode2 { get; set; }
        public string IsoCode3 { get; set; }
      //  public string AddressFormat { get; set; }
        public bool? PostcodeRequired { get; set; }
    }
}
