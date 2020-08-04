namespace ApiTemplate.ViewModels.PackageSubscriberViewModels
{
    public class PublicPackageSubscriber
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }

        public int PaidPackageId { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public int Value { get; set; }

        public bool Status { get; set; }
    }
}
