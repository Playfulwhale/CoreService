namespace ApiTemplate.ViewModels.PackageSubscriberViewModels
{
    public class QueryOptions : PageOptions
    {
        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public int UserId { get; set; }

        public int PaidPackageId { get; set; }

        public string TransactionCode { get; set; }
    }
}
