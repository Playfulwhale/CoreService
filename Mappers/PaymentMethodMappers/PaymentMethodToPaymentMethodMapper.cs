namespace ApiTemplate.Mappers.PaymentMethodMappers
{
    using Boxed.AspNetCore;
    using Boxed.Mapping;
    using Constants;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using System;
    using ViewModels.PaymentMethodViewModels;

    public class PaymentMethodToPaymentMethodMapper : IMapper<Models.PaymentMethod, PaymentMethod>
    {
        //private readonly IHttpContextAccessor httpContextAccessor;
        //private readonly LinkGenerator linkGenerator;

        //public ZoneToZoneMapper(
        //   IHttpContextAccessor httpContextAccessor,
        //   LinkGenerator linkGenerator)
        //{
        //    this.httpContextAccessor = httpContextAccessor;
        //    this.linkGenerator = linkGenerator;
        //}

        public void Map(Models.PaymentMethod source, PaymentMethod destination)
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
            destination.Name = source.Name;
            destination.Status = source.Status;
            destination.LogoUrl = source.LogoUrl;
            destination.Description = source.Description;

        }

    }
}
