namespace ApiTemplate.ViewModels.CountryViewModels
{
    using Swashbuckle.AspNetCore.Annotations;
    using System.ComponentModel.DataAnnotations;

    public class SaveCountry
    {
        /// <summary>
        /// Name of country
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string IsoCode2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string IsoCode3 { get; set; }
        /// <summary>
        /// 
        /// </summary>       
        public string AddressFormat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool? PostcodeRequired { get; set; }    
    }
}
