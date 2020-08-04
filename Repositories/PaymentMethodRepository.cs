namespace ApiTemplate.Repositories
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly DataContext _context;

        public PaymentMethodRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<PaymentMethod> Add(PaymentMethod paymentMethod, CancellationToken cancellationToken)
        {
            await _context.PaymentMethods.AddAsync(paymentMethod, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return paymentMethod;
        }

        public async Task<PaymentMethod> Update(PaymentMethod paymentMethod, CancellationToken cancellationToken)
        {

            var existingPaymentMethod = await _context.PaymentMethods.FirstOrDefaultAsync(x => x.Id == paymentMethod.Id, cancellationToken);
            if (existingPaymentMethod != null)
            {
                existingPaymentMethod.Name = paymentMethod.Name;
                existingPaymentMethod.Status = paymentMethod.Status;
                existingPaymentMethod.Description = paymentMethod.Description;
                existingPaymentMethod.LogoUrl = paymentMethod.LogoUrl;
            }

            _context.Entry(paymentMethod).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return paymentMethod;
        }
        public async Task<List<PaymentMethod>> GetAll(CancellationToken cancellationToken)
        {
            var paymentMethods = await _context.PaymentMethods.ToListAsync(cancellationToken);
            return paymentMethods;
        }

        public async Task<PaymentMethod> Get(int paymentMethodId, CancellationToken cancellationToken)
        {
            var paymentMethod = await _context.PaymentMethods.FirstOrDefaultAsync(x => x.Id == paymentMethodId, cancellationToken);
            return paymentMethod;

        }

        public Task Delete(PaymentMethod paymentMethod, CancellationToken cancellationToken)
        {
            _context.PaymentMethods.Remove(paymentMethod);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

    }
}