namespace ApiTemplate.Commands.PackageSubscriberCommands
{
    using Repositories;
    using ViewModels.PackageSubscriberViewModels;
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using System.Threading;
    using System.Threading.Tasks;

    public class PublicGetAllPackageSubscriberByUserCommand : IPublicGetAllPackageSubscriberByUserCommand
    {
        private readonly IPackageSubscriberRepository _packageSubscriberRepository;
        private readonly IMapper<Models.PackageSubscriber, PublicPackageSubscriber> _packageSubscriberMapper;

        public PublicGetAllPackageSubscriberByUserCommand(
            IPackageSubscriberRepository packageSubscriberRepository,
            IMapper<Models.PackageSubscriber, PublicPackageSubscriber> packageSubscriberMapper)
        {
            _packageSubscriberRepository = packageSubscriberRepository;
            _packageSubscriberMapper = packageSubscriberMapper;
        }

        public async Task<IActionResult> ExecuteAsync(int id, CancellationToken cancellationToken)
        {
            var packageSubscriber = await _packageSubscriberRepository.GetByUser(id, cancellationToken);
            if (packageSubscriber == null)
            {
                return new NoContentResult();
            }
            var packageSubscriberViewModels = _packageSubscriberMapper.MapList(packageSubscriber);

            return new OkObjectResult(packageSubscriberViewModels);
        }
    }
}