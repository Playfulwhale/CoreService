namespace ApiTemplate.Commands.PaidPackageCommands
{
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Constants;
    using Repositories;
    using ViewModels.PaidPackageViewModels;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using System.Linq;

    public class PostPaidPackageCommand : IPostPaidPackageCommand
    {
        private readonly IPaidPackageRepository _paidPackageRepository;
        private readonly IMapper<Models.PaidPackage, PaidPackage> _paidPackageToPaidPackageMapper;
        private readonly IMapper<SavePaidPackage, Models.PaidPackage> _savePaidPackageToPaidPackageMapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PostPaidPackageCommand(
            IPaidPackageRepository paidPackageRepository,
            IMapper<Models.PaidPackage, PaidPackage> paidPackageToPaidPackageMapper,
            IMapper<SavePaidPackage, Models.PaidPackage> savePaidPackageToPaidPackageMapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _paidPackageRepository = paidPackageRepository;
            _paidPackageToPaidPackageMapper = paidPackageToPaidPackageMapper;
            _savePaidPackageToPaidPackageMapper = savePaidPackageToPaidPackageMapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> ExecuteAsync(SavePaidPackage savePaidPackage, CancellationToken cancellationToken)
        {
            var paidPackage = _savePaidPackageToPaidPackageMapper.Map(savePaidPackage);

            //var user = _httpContextAccessor.HttpContext.User;
            //if (user == null)
            //    return new NotFoundResult();

            //var claims = user.Claims.ToList();
            //if (claims.Count < 1)
            //    return new NotFoundResult();
            //// Lấy Id của người dùng
            //var userId = claims.FirstOrDefault(claimRecord => claimRecord.Type == "sub")?.Value;

            //paidPackage.CreatedBy = userId;
            //paidPackage.ModifiedBy = userId;

            paidPackage = await _paidPackageRepository.Add(paidPackage, cancellationToken);
            var paidPackageViewModel = _paidPackageToPaidPackageMapper.Map(paidPackage);

            return new CreatedAtRouteResult(
                PaidPackagesControllerRoute.GetPaidPackage,
                new { id = paidPackageViewModel.Id },
                paidPackageViewModel);
        }
    }
}