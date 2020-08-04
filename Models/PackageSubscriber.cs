namespace ApiTemplate.Models
{
    using System;
    public class PackageSubscriber : BaseModel
    {
        public int PaidPackageId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int UserId { get; set; }

        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }
    }
}
