namespace ApiTemplate.Models
{
    public class Transaction : BaseModel
    {
        public string Method { get; set; }

        public string TransactionCode { get; set; }

        public int Value { get; set; }

        public bool Status { get; set; }
    }
}
