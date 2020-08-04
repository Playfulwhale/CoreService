namespace ApiTemplate.Repositories
{
    using Models;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IPackageSubscriberRepository
    {
        Task<PackageSubscriber> Add(PackageSubscriber packageSubscriber, CancellationToken cancellationToken);

        Task<ICollection<PackageSubscriber>> GetAll(CancellationToken cancellationToken);

        Task<ICollection<PackageSubscriber>> GetByUser(int userId, CancellationToken cancellationToken);

        Task<PackageSubscriber> Get(int id, CancellationToken cancellationToken);

        Task Delete(PackageSubscriber packageSubscriber, CancellationToken cancellationToken);

        Task<Transaction> GetTransactionByTransactionCode(string transactionCode, CancellationToken cancellationToken);
    }
}
