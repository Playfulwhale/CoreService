namespace ApiTemplate.Models
{
    using System;
    public class PaymentMethod : BaseModel
    {

        public string Name { get; set; }

        public string LogoUrl { get; set; }

        public string Description { get; set; }

        public bool Status { get; set; }
    }
}
