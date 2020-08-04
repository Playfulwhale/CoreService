namespace ApiTemplate.ViewModels.PaymentMethodViewModels
{
    public class PaymentMethod
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LogoUrl { get; set; }

        public string Description { get; set; }

        public bool Status { get; set; }
    }
}
