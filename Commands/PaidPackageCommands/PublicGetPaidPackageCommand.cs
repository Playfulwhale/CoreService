namespace ApiTemplate.Commands.PaidPackageCommands
{
    using Repositories;
    using ViewModels.PaidPackageViewModels;
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using System.Threading;
    using System.Threading.Tasks;

    public class PublicGetPaidPackageCommand : IPublicGetPaidPackageCommand
    {
        private readonly IPaidPackageRepository _paidPackageRepository;
        private readonly IMapper<Models.PaidPackage, PublicPaidPackage> _paidPackageMapper;

        public PublicGetPaidPackageCommand(
            IPaidPackageRepository paidPackageRepository,
            IMapper<Models.PaidPackage, PublicPaidPackage> paidPackageMapper)
        {
            _paidPackageRepository = paidPackageRepository;
            _paidPackageMapper = paidPackageMapper;
        }

        public async Task<IActionResult> ExecuteAsync(int paidPackageId, CancellationToken cancellationToken)
        {
            var paidPackage = await _paidPackageRepository.PublicGet(paidPackageId, cancellationToken);
            if (paidPackage == null)
            {
                return new NoContentResult();
            }
            var paidPackageViewModels = _paidPackageMapper.Map(paidPackage);

            return new OkObjectResult(paidPackageViewModels);
        }
    }
}