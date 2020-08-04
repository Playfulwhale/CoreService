using System;
using System.Globalization;
using ApiTemplate.Services;
using ApiTemplate.ViewModels.PackageSubscriberViewModels;
using Boxed.Mapping;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.Mappers.PackageSubscriberMappers
{
    public class PackageSubscriberToSavePackageSubscriberMapper : IMapper<SavePackageSubscriber, Models.PackageSubscriber>
    {
        private readonly IClockService _clockService;
        public PackageSubscriberToSavePackageSubscriberMapper(
            IClockService clockService)
        {
            _clockService = clockService;
        }

        public void Map(SavePackageSubscriber source, Models.PackageSubscriber destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }
            var now = _clockService.UtcNow;

            if (destination.CreatedAt == null)
            {
                destination.CreatedAt = now;
            }
            destination.ModifiedAt = now;
            destination.PaidPackageId = source.PaidPackageId;
            destination.StartDate = DateTime.ParseExact(source.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            destination.EndDate = DateTime.ParseExact(source.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            destination.UserId = source.UserId;
            destination.Transaction = new Models.Transaction
            {
                TransactionCode = source.TransactionCode,
                Method = source.Method,
                Value = source.Value,
                Status = source.Status,
                CreatedAt = destination.CreatedAt == null ? now : destination.CreatedAt,
                ModifiedAt = now,
            };
        }

    }
}
