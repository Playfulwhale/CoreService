namespace ApiTemplate.Commands.PackageSubscriberCommands
{
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Constants;
    using Repositories;
    using ViewModels.PackageSubscriberViewModels;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using System.Linq;

    public class PostPackageSubscriberCommand : IPostPackageSubscriberCommand
    {
        private readonly IPackageSubscriberRepository _packageSubscriberRepository;
        private readonly IMapper<Models.PackageSubscriber, PackageSubscriber> _packageSubscriberToPackageSubscriberMapper;
        private readonly IMapper<SavePackageSubscriber, Models.PackageSubscriber> _savePackageSubscriberToPackageSubscriberMapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PostPackageSubscriberCommand(
            IPackageSubscriberRepository packageSubscriberRepository,
            IMapper<Models.PackageSubscriber, PackageSubscriber> packageSubscriberToPackageSubscriberMapper,
            IMapper<SavePackageSubscriber, Models.PackageSubscriber> savePackageSubscriberToPackageSubscriberMapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _packageSubscriberRepository = packageSubscriberRepository;
            _packageSubscriberToPackageSubscriberMapper = packageSubscriberToPackageSubscriberMapper;
            _savePackageSubscriberToPackageSubscriberMapper = savePackageSubscriberToPackageSubscriberMapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> ExecuteAsync(SavePackageSubscriber savePackageSubscriber, CancellationToken cancellationToken)
        {
            var transaction = await _packageSubscriberRepository.GetTransactionByTransactionCode(savePackageSubscriber.TransactionCode, cancellationToken);
            if (transaction != null)
                return new ConflictResult();
            var packageSubscriber = _savePackageSubscriberToPackageSubscriberMapper.Map(savePackageSubscriber);

            //var user = _httpContextAccessor.HttpContext.User;
            //if (user == null)
            //    return new NotFoundResult();

            //var claims = user.Claims.ToList();
            //if (claims.Count < 1)
            //    return new NotFoundResult();
            //// Lấy Id của người dùng
            //var userId = claims.FirstOrDefault(claimRecord => claimRecord.Type == "sub")?.Value;

            //packageSubscriber.CreatedBy = userId;
            //packageSubscriber.ModifiedBy = userId;
            packageSubscriber = await _packageSubscriberRepository.Add(packageSubscriber, cancellationToken);
            var packageSubscriberViewModel = _packageSubscriberToPackageSubscriberMapper.Map(packageSubscriber);

            return new CreatedAtRouteResult(
                PackageSubscribersControllerRoute.GetPackageSubscriber,
                new { id = packageSubscriberViewModel.Id },
                packageSubscriberViewModel);
        }
    }
}