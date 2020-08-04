namespace ApiTemplate.ViewModels.PackageSubscriberViewModels
{
    public class SavePackageSubscriber
    {
        public string TransactionCode { get; set; }

        public int UserId { get; set; }

        public int PaidPackageId { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string Method { get; set; }

        public int Value { get; set; }

        public bool Status { get; set; }
    }
}
