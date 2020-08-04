namespace ApiTemplate.ViewModels.CurrencyViewModels
{
    using System.ComponentModel.DataAnnotations;
    using Swashbuckle.AspNetCore.Annotations;
    public class SaveCurrency
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Code { get; set; }

        public string SymbolLeft { get; set; }

        public string SymbolRight { get; set; }

        [Required]
        public double? Value { get; set; }

        public bool Default { get; set; }

        public bool Active { get; set; }
    }
}
