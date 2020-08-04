namespace ApiTemplate.Commands.PaidPackageCommands
{
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Repositories;
    using System.Threading;
    using System.Threading.Tasks;
    using ViewModels.PaidPackageViewModels;

    public class GetAllPaidPackageCommand : IGetAllPaidPackageCommand
    {
        private readonly IPaidPackageRepository _paidPackageRepository;
        private readonly IMapper<Models.PaidPackage, PaidPackage> _paidPackageMapper;

        public GetAllPaidPackageCommand(
            IPaidPackageRepository paidPackageRepository,
            IMapper<Models.PaidPackage, PaidPackage> paidPackageMapper)
        {
            _paidPackageRepository = paidPackageRepository;
            _paidPackageMapper = paidPackageMapper;
        }

        public async Task<IActionResult> ExecuteAsync(CancellationToken cancellationToken)
        {
            var listPaidPackage = await _paidPackageRepository.GetAll(cancellationToken);
            var paidPackageViewModels = _paidPackageMapper.MapList(listPaidPackage);
            
            return new OkObjectResult(paidPackageViewModels);
        }
    }
}