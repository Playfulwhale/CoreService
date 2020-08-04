namespace ApiTemplate.Repositories
{
    using Models;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IMenuRepository
    {
        Task<List<Menu>> GetAll(CancellationToken cancellationToken);

        Task<Menu> Get(int menuId, CancellationToken cancellationToken);

        Task<Menu> GetByPosition(string position, CancellationToken cancellationToken);

        Task<Menu> Add(Menu menu, CancellationToken cancellationToken);

        Task<Menu> Update(Menu menu, CancellationToken cancellationToken);

        Task Delete(Menu menu, CancellationToken cancellationToken);


    }
}
