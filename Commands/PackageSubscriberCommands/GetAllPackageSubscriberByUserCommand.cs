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

    public class GetAllPackageSubscriberByUserCommand : IGetAllPackageSubscriberByUserCommand
    {
        private readonly IPackageSubscriberRepository _packageSubscriberRepository;
        private readonly IMapper<Models.PackageSubscriber, PackageSubscriber> _packageSubscriberMapper;

        public GetAllPackageSubscriberByUserCommand(
            IPackageSubscriberRepository packageSubscriberRepository,
            IMapper<Models.PackageSubscriber, PackageSubscriber> packageSubscriberMapper)
        {
            _packageSubscriberRepository = packageSubscriberRepository;
            _packageSubscriberMapper = packageSubscriberMapper;
        }

        public async Task<IActionResult> ExecuteAsync(int userId, QueryOptions queryOptions, CancellationToken cancellationToken)
        {
            var listPackageSubscriberByUser = await _packageSubscriberRepository.GetByUser(userId, cancellationToken);
            
            if (queryOptions.StartDate != null) listPackageSubscriberByUser = listPackageSubscriberByUser.Where(x => x.StartDate >= DateTime.ParseExact(queryOptions.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)).ToList();
            if (queryOptions.EndDate != null) listPackageSubscriberByUser = listPackageSubscriberByUser.Where(x => x.EndDate <= DateTime.ParseExact(queryOptions.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)).ToList();
            if (listPackageSubscriberByUser == null) return new NoContentResult();

            var totalRecord = listPackageSubscriberByUser.Count();

            var packageSubscriberViewModels = _packageSubscriberMapper.MapList(listPackageSubscriberByUser);

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