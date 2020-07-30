namespace ApiTemplate.Repositories
{
    using Models;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IContactUsRepository
    {
        Task<ContactUs> Add(ContactUs contactUs, CancellationToken cancellationToken);

        Task<ContactUs> Update(ContactUs contactUs, CancellationToken cancellationToken);

        Task Delete(ContactUs contactUs, CancellationToken cancellationToken);

        Task<ContactUs> Get(int id, CancellationToken cancellationToken);

        Task<List<ContactUs>> GetAll(CancellationToken cancellationToken);
    }
}