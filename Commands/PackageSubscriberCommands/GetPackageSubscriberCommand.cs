namespace ApiTemplate.Commands.PackageSubscriberCommands
{
    using Repositories;
    using ViewModels.PackageSubscriberViewModels;
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetPackageSubscriberCommand : IGetPackageSubscriberCommand
    {
        private readonly IPackageSubscriberRepository _packageSubscriberRepository;
        private readonly IMapper<Models.PackageSubscriber, PackageSubscriber> _packageSubscriberMapper;

        public GetPackageSubscriberCommand(
            IPackageSubscriberRepository packageSubscriberRepository,
            IMapper<Models.PackageSubscriber, PackageSubscriber> packageSubscriberMapper)
        {
            _packageSubscriberRepository = packageSubscriberRepository;
            _packageSubscriberMapper = packageSubscriberMapper;
        }

        public async Task<IActionResult> ExecuteAsync(int id, CancellationToken cancellationToken)
        {
            var packageSubscriber = await _packageSubscriberRepository.Get(id, cancellationToken);
            if (packageSubscriber == null)
            {
                return new NoContentResult();
            }
            var packageSubscriberViewModels = _packageSubscriberMapper.Map(packageSubscriber);

            return new OkObjectResult(packageSubscriberViewModels);
        }
    }
}