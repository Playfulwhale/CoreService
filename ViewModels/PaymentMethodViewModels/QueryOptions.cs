namespace ApiTemplate.ViewModels.PaymentMethodViewModels
{
    public class QueryOptions : PageOptions
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LogoUrl { get; set; }

        public string Description { get; set; }

        public bool Status { get; set; }
    }
}
