namespace ApiTemplate.Commands.PaidPackageCommands
{
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Repositories;
    using ViewModels.PaidPackageViewModels;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using System.Linq;

    public class PutPaidPackageCommand : IPutPaidPackageCommand
    {
        private readonly IPaidPackageRepository _paidPackageRepository;
        private readonly IMapper<Models.PaidPackage, PaidPackage> _paidPackageToPaidPackageMapper;
        private readonly IMapper<SavePaidPackage, Models.PaidPackage> _putPaidPackageToPaidPackageMapper;

        public PutPaidPackageCommand(
            IPaidPackageRepository paidPackageRepository,
            IMapper<Models.PaidPackage, PaidPackage> paidPackageToPaidPackageMapper,
            IMapper<SavePaidPackage, Models.PaidPackage> putPaidPackageToPaidPackageMapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _paidPackageRepository = paidPackageRepository;
            _paidPackageToPaidPackageMapper = paidPackageToPaidPackageMapper;
            _putPaidPackageToPaidPackageMapper = putPaidPackageToPaidPackageMapper;
        }

        public async Task<IActionResult> ExecuteAsync(int id, SavePaidPackage savePaidPackage, CancellationToken cancellationToken)
        {
            var paidPackage = await _paidPackageRepository.Get(id, cancellationToken);
            if (paidPackage == null)
            {
                return new NoContentResult();
            }

            _putPaidPackageToPaidPackageMapper.Map(savePaidPackage, paidPackage);

            //var user = _httpContextAccessor.HttpContext.User;
            //if (user == null)
            //    return new NotFoundResult();

            //var claims = user.Claims.ToList();
            //if (claims.Count < 1)
            //    return new NotFoundResult();
            //// Lấy Id của người dùng
            //var userId = claims.FirstOrDefault(claimRecord => claimRecord.Type == "sub")?.Value;

            //paidPackage.ModifiedBy = userId;

            paidPackage = await _paidPackageRepository.Update(paidPackage, cancellationToken);
            var paidPackageViewModel = _paidPackageToPaidPackageMapper.Map(paidPackage);

            return new OkObjectResult(paidPackageViewModel);
        }
    }
}