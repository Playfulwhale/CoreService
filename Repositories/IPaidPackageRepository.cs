namespace ApiTemplate.Repositories
{
    using Models;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IPaidPackageRepository
    {
        Task<PaidPackage> Add(PaidPackage paidPackage, CancellationToken cancellationToken);

        Task<ICollection<PaidPackage>> GetAll(CancellationToken cancellationToken);

        Task<PaidPackage> Get(int paidPackageId, CancellationToken cancellationToken);

        Task<PaidPackage> Update(PaidPackage paidPackage, CancellationToken cancellationToken);

        Task<PaidPackage> PublicGet(int paidPackageId, CancellationToken cancellationToken);

        Task Delete(PaidPackage paidPackage, CancellationToken cancellationToken);
    }
}
