namespace ApiTemplate.Commands.PackageSubscriberCommands
{
    using Repositories;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading;
    using System.Threading.Tasks;

    public class DeletePackageSubscriberCommand : IDeletePackageSubscriberCommand
    {
        private readonly IPackageSubscriberRepository _packageSubscriberRepository;

        public DeletePackageSubscriberCommand(IPackageSubscriberRepository packageSubscriberRepository)
        {
            _packageSubscriberRepository = packageSubscriberRepository;
        }

        public async Task<IActionResult> ExecuteAsync(int packageSubscriberId, CancellationToken cancellationToken)
        {
            var packageSubscriber = await _packageSubscriberRepository.Get(packageSubscriberId, cancellationToken);
            if (packageSubscriber == null)
            {
                return new NoContentResult();
            }

            await _packageSubscriberRepository.Delete(packageSubscriber, cancellationToken);

            return new NoContentResult();
        }
    }
}