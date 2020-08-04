namespace ApiTemplate.Models
{
    public class Currency : BaseModel
    {
        public string Title { get; set; }
      
        public string Code { get; set; }
        
        public string SymbolLeft { get; set; }
       
        public string SymbolRight { get; set; }
        
        public double? Value { get; set; }

        public bool Default { get; set; }

    }
}