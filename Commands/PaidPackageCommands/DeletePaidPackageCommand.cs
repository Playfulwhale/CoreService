namespace ApiTemplate.Commands.PaidPackageCommands
{
    using Repositories;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading;
    using System.Threading.Tasks;

    public class DeletePaidPackageCommand : IDeletePaidPackageCommand
    {
        private readonly IPaidPackageRepository _paidPackageRepository;

        public DeletePaidPackageCommand(IPaidPackageRepository paidPackageRepository)
        {
            _paidPackageRepository = paidPackageRepository;
        }

        public async Task<IActionResult> ExecuteAsync(int paidPackageId, CancellationToken cancellationToken)
        {
            var paidPackage = await _paidPackageRepository.Get(paidPackageId, cancellationToken);
            if (paidPackage == null)
            {
                return new NoContentResult();
            }

            await _paidPackageRepository.Delete(paidPackage, cancellationToken);

            return new NoContentResult();
        }
    }
}