namespace ApiTemplate.Repositories
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Models;

    public interface IMenuItemRepository
    {
        Task<MenuItem> Add(MenuItem menuItem, CancellationToken cancellationToken);

        Task<List<MenuItem>> GetAll(CancellationToken cancellationToken);
        
        Task<MenuItem> Get(int menuId, CancellationToken cancellationToken);

        Task<Menu> GetMenuId(int menuId, CancellationToken cancellationToken);

        Task<List<MenuItem>> GetByMenu(int menuId, CancellationToken cancellationToken);

        Task<MenuItem> Update(MenuItem menuItem, CancellationToken cancellationToken);

        Task Delete(MenuItem menuItem, CancellationToken cancellationToken);

        Task<List<MenuItem>> PublicGetByMenu(int menuId, CancellationToken cancellationToken);

        Task<List<MenuItem>> GetChildrens(int menuId, CancellationToken cancellationToken);

        Task<List<MenuItem>> GetMenuItemsByListId(List<int> listMenuId, CancellationToken cancellationToken);

        Task DeleteRange(List<MenuItem> menuItem, CancellationToken cancellationToken);
    }
}
