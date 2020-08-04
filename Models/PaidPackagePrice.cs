namespace ApiTemplate.Models
{
    public class PaidPackagePrice : BaseModel
    {
        public int PaidPackageId { get; set; }

        public int Price { get; set; }

        public string Currency { get; set; }

        public string Period { get; set; }
    }
}
