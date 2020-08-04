namespace ApiTemplate.Commands.PackageSubscriberCommands
{
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Constants;
    using Repositories;
    using ViewModels.PackageSubscriberViewModels;
    using System.Threading;
    using System.Threading.Tasks;
    using System;
    using System.Linq;

    public class PublicPostPackageSubscriberCommand : IPublicPostPackageSubscriberCommand
    {
        private readonly IPackageSubscriberRepository _packageSubscriberRepository;
        private readonly IPaidPackageRepository _paidPackageRepository;
        private readonly IMapper<Models.PackageSubscriber, PackageSubscriber> _packageSubscriberToPackageSubscriberMapper;
        private readonly IMapper<SavePackageSubscriber, Models.PackageSubscriber> _savePackageSubscriberToPackageSubscriberMapper;

        public PublicPostPackageSubscriberCommand(
            IPackageSubscriberRepository packageSubscriberRepository,
            IPaidPackageRepository paidPackageRepository,
            IMapper<Models.PackageSubscriber, PackageSubscriber> packageSubscriberToPackageSubscriberMapper,
            IMapper<SavePackageSubscriber, Models.PackageSubscriber> savePackageSubscriberToPackageSubscriberMapper)
        {
            _packageSubscriberRepository = packageSubscriberRepository;
            _paidPackageRepository = paidPackageRepository;
            _packageSubscriberToPackageSubscriberMapper = packageSubscriberToPackageSubscriberMapper;
            _savePackageSubscriberToPackageSubscriberMapper = savePackageSubscriberToPackageSubscriberMapper;
        }

        public async Task<IActionResult> ExecuteAsync(PublicSavePackageSubscriber publicSavePackageSubscriber, CancellationToken cancellationToken)
        {
            var transactionCode = "ABC123XYZ";
            var method = "Momo";
            var userId = 0;
            //var transaction = await _packageSubscriberRepository.GetTransactionByTransactionCode(transactionCode, cancellationToken);
            //if (transaction != null)
            //    return new ConflictResult();


            var paidPackage = await _paidPackageRepository.Get(publicSavePackageSubscriber.PaidPackageId, cancellationToken);
            if (paidPackage == null) return new NoContentResult();

            var savePackageSubscriber = new SavePackageSubscriber()
            {
                TransactionCode = transactionCode,
                UserId = userId,
                PaidPackageId = paidPackage.Id,
                StartDate = DateTime.Now.ToString("dd/MM/yyyy"),
                EndDate = publicSavePackageSubscriber.Period == "Y" ? DateTime.Now.AddYears(1).ToString("dd/MM/yyyy") : DateTime.Now.AddMonths(1).ToString("dd/MM/yyyy"),
                Method = method,
                Value = publicSavePackageSubscriber.Period == "Y" ? getCurrency(paidPackage, "Y") : getCurrency(paidPackage, "M"),
                Status = true
            };

            var packageSubscriber = _savePackageSubscriberToPackageSubscriberMapper.Map(savePackageSubscriber);
            packageSubscriber = await _packageSubscriberRepository.Add(packageSubscriber, cancellationToken);
            var packageSubscriberViewModel = _packageSubscriberToPackageSubscriberMapper.Map(packageSubscriber);

            return new CreatedAtRouteResult(
                PackageSubscribersControllerRoute.GetPackageSubscriber,
                new { id = packageSubscriberViewModel.Id },
                packageSubscriberViewModel);
        }

        public int getCurrency(Models.PaidPackage paidPackage, string period)
        {
            var price = -1;
            if(paidPackage.paidPackagePrices != null)
            {
                var paidPackagePrice = paidPackage.paidPackagePrices.FirstOrDefault(x => x.Period == period);
                price = paidPackagePrice != null ? paidPackagePrice.Price : -1;
            }
            return price;
        }
    }
}