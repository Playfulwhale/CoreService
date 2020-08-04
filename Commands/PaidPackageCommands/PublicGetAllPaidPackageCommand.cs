namespace ApiTemplate.Commands.PaidPackageCommands
{
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Repositories;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using ViewModels.PaidPackageViewModels;

    public class PublicGetAllPaidPackageCommand : IPublicGetAllPaidPackageCommand
    {
        private readonly IPaidPackageRepository _paidPackageRepository;
        private readonly IMapper<Models.PaidPackage, PublicPaidPackage> _paidPackageMapper;

        public PublicGetAllPaidPackageCommand(
            IPaidPackageRepository paidPackageRepository,
            IMapper<Models.PaidPackage, PublicPaidPackage> paidPackageMapper)
        {
            _paidPackageRepository = paidPackageRepository;
            _paidPackageMapper = paidPackageMapper;
        }

        public async Task<IActionResult> ExecuteAsync(CancellationToken cancellationToken)
        {
            var listPaidPackage = await _paidPackageRepository.GetAll(cancellationToken);
            listPaidPackage = listPaidPackage.Where(x => x.Status).ToList();
            var paidPackageViewModels = _paidPackageMapper.MapList(listPaidPackage);
            return new OkObjectResult(paidPackageViewModels);
        }
    }
}