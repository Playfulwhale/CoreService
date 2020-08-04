namespace ApiTemplate.ViewModels.CurrencyViewModels
{
    using Swashbuckle.AspNetCore.Annotations;
    
    public class Currency
    {
        
        public int Id { get; set; }

        public string Title { get; set; }

        public string Code { get; set; }

        public string SymbolLeft { get; set; }

        public string SymbolRight { get; set; }

        public double? Value { get; set; }

        public bool Default { get; set; }

        public bool Active { get; set; }

        public string Url { get; set; }
    }
}
