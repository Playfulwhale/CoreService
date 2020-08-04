namespace ApiTemplate.Mappers.PackageSubscriberMappers
{
    using Boxed.Mapping;
    using Constants;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using System;
    using ViewModels.PackageSubscriberViewModels;

    public class PackageSubscriberToPackageSubscriberMapper : IMapper<Models.PackageSubscriber, PackageSubscriber>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly LinkGenerator linkGenerator;

        public PackageSubscriberToPackageSubscriberMapper(
           IHttpContextAccessor httpContextAccessor,
           LinkGenerator linkGenerator)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.linkGenerator = linkGenerator;
        }
        public void Map(Models.PackageSubscriber source, PackageSubscriber destination)
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
            destination.TransactionCode = source.Transaction.TransactionCode;
            destination.Method = source.Transaction.Method;
            destination.Value = source.Transaction.Value;
            destination.Status = source.Transaction.Status;
            //destination.Url = _urlHelper.AbsoluteRouteUrl(PackageSubscribersControllerRoute.GetPackageSubscriber, new { source.Id });
            destination.Url = new Uri(this.linkGenerator.GetUriByRouteValues(
                this.httpContextAccessor.HttpContext,
                PackageSubscribersControllerRoute.GetPackageSubscriber,
                new { source.Id })).ToString();
        }

    }
}
