namespace ApiTemplate.Mappers.PackageSubscriberMappers
{
    using Boxed.Mapping;
    using System;
    using ViewModels.PackageSubscriberViewModels;

    public class PackageSubscriberToPublicPackageSubscriberMapper : IMapper<Models.PackageSubscriber, PublicPackageSubscriber>
    {

        public void Map(Models.PackageSubscriber source, PublicPackageSubscriber destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            destination.Id = source.Id;
            destination.PaidPackageId = source.PaidPackageId;
            destination.StartDate = source.StartDate.ToString("dd/MM/yyyy");
            destination.EndDate = source.EndDate.ToString("dd/MM/yyyy");
            destination.UserId = source.UserId;
            destination.Value = source.Transaction.Value;
            destination.Status = source.Transaction.Status;
        }

    }
}
