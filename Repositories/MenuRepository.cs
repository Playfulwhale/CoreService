namespace ApiTemplate.Repositories
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class MenuRepository : IMenuRepository
    {
        private readonly DataContext _context;

        public MenuRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Menu> Get(int menuId, CancellationToken cancellationToken)
        {
            var menu = await _context.Menus.Include(x => x.menuItems).SingleOrDefaultAsync(x => x.Id == menuId, cancellationToken);
            foreach (var menuItem in menu.menuItems)
            {
                var menuItemDescriptions = await _context.MenuItemDescriptions.Where(x => x.MenuItemId == menuItem.Id).ToListAsync(cancellationToken);
                if (menuItemDescriptions != null)
                {
                    menuItem.Descriptions = menuItemDescriptions;
                }
            }
            return menu;

        }

        public async Task<Menu> Add(Menu menu, CancellationToken cancellationToken)
        {
            await _context.Menus.AddAsync(menu, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return menu;
        }

        public async Task<Menu> GetByPosition(string position, CancellationToken cancellationToken)
        {
            var menu = await _context.Menus.FirstOrDefaultAsync(x => x.Position.ToLower() == position.ToLower(), cancellationToken);
            return menu;
        }

        public async Task<Menu> Update(Menu menu, CancellationToken cancellationToken)
        {
            _context.Menus.Update(menu);
            await _context.SaveChangesAsync(cancellationToken);
            return menu;
        }

        public Task Delete(Menu menu, CancellationToken cancellationToken)
        {
            if (!_context.Menus.Contains(menu)) return Task.CompletedTask;
            _context.Menus.Remove(menu);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<List<Menu>> GetAll(CancellationToken cancellationToken)
        {
            var menus = await _context.Menus.ToListAsync(cancellationToken);
            foreach(var menu in menus)
            {
                menu.menuItems = await _context.MenuItems.Include(x => x.Descriptions).Where(x => x.MenuId == menu.Id).OrderBy(x => x.Order).ToListAsync(cancellationToken);
            }
            return menus;
        }
        
    }
}
