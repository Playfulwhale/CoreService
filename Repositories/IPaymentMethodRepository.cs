namespace ApiTemplate.Repositories
{
    using Models;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IPaymentMethodRepository
    {
        Task<PaymentMethod> Add(PaymentMethod paymentMethod, CancellationToken cancellationToken);

        Task<List<PaymentMethod>> GetAll(CancellationToken cancellationToken);

        Task<PaymentMethod> Get(int paymentMethodId, CancellationToken cancellationToken);

        Task<PaymentMethod> Update(PaymentMethod paymentMethod, CancellationToken cancellationToken);

        Task Delete(PaymentMethod paymentMethod, CancellationToken cancellationToken);
    }
}
