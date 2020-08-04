namespace ApiTemplate.Repositories
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class PackageSubscriberRepository : IPackageSubscriberRepository
    {
        private readonly DataContext _context;

        public PackageSubscriberRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<PackageSubscriber> Add(PackageSubscriber packageSubscriber, CancellationToken cancellationToken)
        {
            await _context.PackageSubscribers.AddAsync(packageSubscriber, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return packageSubscriber;
        }

        public async Task<PackageSubscriber> Get(int id, CancellationToken cancellationToken)
        {
            var packageSubscriber = await _context.PackageSubscribers.Include(x => x.Transaction).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            return packageSubscriber;
        }

        public async Task<ICollection<PackageSubscriber>> GetByUser(int userId, CancellationToken cancellationToken)
        {
            var packageSubscribers = await _context.PackageSubscribers.Include(x => x.Transaction).Where(x => x.UserId == userId).OrderByDescending(x => x.Id).ToListAsync(cancellationToken);
            return packageSubscribers;
        }

        public async Task<Transaction> GetTransactionByTransactionCode(string transactionCode, CancellationToken cancellationToken)
        {
            var transaction = await _context.Transactions.FirstOrDefaultAsync(x => x.TransactionCode == transactionCode, cancellationToken);
            return transaction;
        }

        public async Task<ICollection<PackageSubscriber>> GetAll(CancellationToken cancellationToken)
        {
            var packageSubscribers = await _context.PackageSubscribers.Include(x => x.Transaction).OrderByDescending(x => x.Id).ToListAsync(cancellationToken);
            return packageSubscribers;
        }

        public Task Delete(PackageSubscriber packageSubscriber, CancellationToken cancellationToken)
        {
            _context.PackageSubscribers.Remove(packageSubscriber);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

    }
}