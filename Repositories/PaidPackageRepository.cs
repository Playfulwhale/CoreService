namespace ApiTemplate.Repositories
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class PaidPackageRepository : IPaidPackageRepository
    {
        private readonly DataContext _context;

        public PaidPackageRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<PaidPackage> Add(PaidPackage paidPackage, CancellationToken cancellationToken)
        {
            await _context.PaidPackages.AddAsync(paidPackage, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return paidPackage;
        }

        public async Task<PaidPackage> Update(PaidPackage paidPackage, CancellationToken cancellationToken)
        {

            var paidPackagePrice = _context.PaidPackagePrices.AsNoTracking().ToList();
            var paidPackagePricesToAdd = new List<PaidPackagePrice>(); // List dùng để thêm mới nếu chưa tồn tại
            var listPaidPackagePrices = paidPackage.paidPackagePrices.ToList();
            foreach (var item in listPaidPackagePrices)
            {
                // Kiểm tra tồn tại trong DB
                var itemExist = paidPackagePrice.FirstOrDefault(x => x.Period == item.Period && x.PaidPackageId == paidPackage.Id);
                if (itemExist == null)
                {
                    item.PaidPackageId = paidPackage.Id;
                    paidPackagePricesToAdd.Add(item);
                    paidPackage.paidPackagePrices.Remove(item);
                }
                else { item.Id = itemExist.Id; }
            }
            _context.PaidPackages.Update(paidPackage);
            await _context.PaidPackagePrices.AddRangeAsync(paidPackagePricesToAdd, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return paidPackage;
        }

        public async Task<ICollection<PaidPackage>> GetAll(CancellationToken cancellationToken)
        {
            var paidPackages = await _context.PaidPackages.ToListAsync(cancellationToken);
            var paidPackagePrices = await _context.PaidPackagePrices.ToListAsync(cancellationToken);
            foreach (var paidPackage in paidPackages)
            {
                paidPackage.paidPackagePrices = paidPackagePrices.Where(x => x.PaidPackageId == paidPackage.Id).ToList();
            }
            return paidPackages;
        }

        public async Task<PaidPackage> Get(int paidPackageId, CancellationToken cancellationToken)
        {
            var paidPackage = await _context.PaidPackages.AsNoTracking().FirstOrDefaultAsync(x => x.Id == paidPackageId, cancellationToken);
            var paidPackagePrices = await _context.PaidPackagePrices.AsNoTracking().ToListAsync(cancellationToken);
            if(paidPackage != null)
                paidPackage.paidPackagePrices = paidPackagePrices.Where(x => x.PaidPackageId == paidPackage.Id).ToList();
            return paidPackage;
        }

        public async Task<PaidPackage> PublicGet(int paidPackageId, CancellationToken cancellationToken)
        {
            var paidPackage = await _context.PaidPackages.AsNoTracking().FirstOrDefaultAsync(x => x.Id == paidPackageId && x.Status, cancellationToken);
            var paidPackagePrices = await _context.PaidPackagePrices.AsNoTracking().ToListAsync(cancellationToken);
            if (paidPackage != null)
                paidPackage.paidPackagePrices = paidPackagePrices.Where(x => x.PaidPackageId == paidPackage.Id).ToList();
            return paidPackage;
        }

        public Task Delete(PaidPackage paidPackage, CancellationToken cancellationToken)
        {
            _context.PaidPackages.Remove(paidPackage);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

    }
}