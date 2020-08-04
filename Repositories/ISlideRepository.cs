namespace ApiTemplate.Repositories
{
    using Models;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ISlideRepository
    {
        Task<Slide> Add(Slide slide, CancellationToken cancellationToken);

        Task<Slide> Update(Slide slide, CancellationToken cancellation);

        Task Delete(Slide slide, CancellationToken cancellation);

        Task<Slide> Get(int id, CancellationToken cancellation);

        Task<ICollection<Slide>> GetAll(CancellationToken cancellation);

        Task<ICollection<Slide>> PublicGetAll(CancellationToken cancellation);
    }
}
