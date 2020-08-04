namespace ApiTemplate.Mappers.PaymentMethodMappers
{
    using System;
    using Services;
    using Boxed.Mapping;
    using ViewModels.PaymentMethodViewModels;

    public class PaymentMethodToSavePaymentMethodMapper : IMapper<Models.PaymentMethod, SavePaymentMethod>, IMapper<SavePaymentMethod, Models.PaymentMethod>
    {
        private readonly IClockService clockService;

        public PaymentMethodToSavePaymentMethodMapper(IClockService clockService) =>
            this.clockService = clockService;

        public void Map(Models.PaymentMethod source, SavePaymentMethod destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            destination.Name = source.Name;
            destination.Status = source.Status;
            destination.LogoUrl = source.LogoUrl;
            destination.Description = source.Description;

        }

        public void Map(SavePaymentMethod source, Models.PaymentMethod destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            var now = clockService.UtcNow;

            if (destination.CreatedAt == DateTimeOffset.MinValue)
            {
                destination.CreatedAt = now;
            }

            destination.Name = source.Name;
            destination.Status = source.Status;
            destination.LogoUrl = source.LogoUrl;
            destination.Description = source.Description;
            destination.ModifiedAt = now;
        }
    }
}
