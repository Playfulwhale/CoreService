namespace ApiTemplate.Commands.PackageSubscriberCommands
{
    using Constants;
    using Boxed.AspNetCore;
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Repositories;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using ViewModels.PackageSubscriberViewModels;

    public class GetAllPackageSubscriberCommand : IGetAllPackageSubscriberCommand
    {
        private readonly IPackageSubscriberRepository _packageSubscriberRepository;
        private readonly IMapper<Models.PackageSubscriber, PackageSubscriber> _packageSubscriberMapper;
        public GetAllPackageSubscriberCommand(
            IPackageSubscriberRepository packageSubscriberRepository,
            IMapper<Models.PackageSubscriber, PackageSubscriber> packageSubscriberMapper)
        {
            _packageSubscriberRepository = packageSubscriberRepository;
            _packageSubscriberMapper = packageSubscriberMapper;
        }

        public async Task<IActionResult> ExecuteAsync(QueryOptions queryOptions, CancellationToken cancellationToken)
        {
            var listPackageSubscriberAll = await _packageSubscriberRepository.GetAll(cancellationToken);
            var listPackageSubscriber = listPackageSubscriberAll
                .Skip(queryOptions.Count.Value * (queryOptions.Page.Value - 1))
                .Take(queryOptions.Count.Value)
                .ToList();
            if (queryOptions.TransactionCode != null) listPackageSubscriber = listPackageSubscriber.Where(x => x.Transaction.TransactionCode == queryOptions.TransactionCode).ToList();
            if (queryOptions.UserId != 0) listPackageSubscriber = listPackageSubscriber.Where(x => x.UserId == queryOptions.UserId).ToList();
            if (queryOptions.StartDate != null) listPackageSubscriber = listPackageSubscriber.Where(x => x.StartDate >= DateTime.ParseExact(queryOptions.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)).ToList();
            if (queryOptions.EndDate != null) listPackageSubscriber = listPackageSubscriber.Where(x => x.EndDate <= DateTime.ParseExact(queryOptions.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)).ToList();
            if (queryOptions.PaidPackageId != 0) listPackageSubscriber = listPackageSubscriber.Where(x => x.PaidPackageId == queryOptions.PaidPackageId).ToList();
            if (listPackageSubscriber == null) return new NoContentResult();

            var totalRecord = listPackageSubscriberAll.Count();

            var packageSubscriberViewModels = _packageSubscriberMapper.MapList(listPackageSubscriber);

            var (totalCount, totalPages) = await GetTotalPages(queryOptions.Count.Value, totalRecord);

            var page = new PageResult<PackageSubscriber>()
            {
                Count = queryOptions.Count.Value,
                Items = packageSubscriberViewModels,
                Page = queryOptions.Page.Value,
                TotalCount = totalCount,
                TotalPages = totalPages,
            };

            return new OkObjectResult(page);
        }


        private async Task<(int totalCount, int totalPages)> GetTotalPages(int count, int totalCount)
        {
            var totalPages = (int)Math.Ceiling(totalCount / (double)count);
            return await Task.FromResult((totalCount, totalPages));
        }
    }
}