namespace ApiTemplate.Repositories
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly DataContext _context;

        public MenuItemRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<MenuItem>> GetByMenu(int menuId, CancellationToken cancellationToken)
        {
            var menuItems = await _context.MenuItems.Where(x => x.MenuId == menuId).OrderBy(x => x.Order).ToListAsync(cancellationToken);
            if (menuItems == null) return null;
            {
                foreach (var menuItem in menuItems)
                {
                    var menuItemDescriptions = await _context.MenuItemDescriptions.Where(x => x.MenuItemId == menuItem.Id).ToListAsync(cancellationToken);
                    if (menuItemDescriptions != null)
                    {
                        menuItem.Descriptions = menuItemDescriptions;
                    }
                }
            }
            return menuItems;
        }
        public async Task<Menu> GetMenuId(int menuId, CancellationToken cancellationToken)
        { 
            var menu = await _context.Menus.FirstOrDefaultAsync(x => x.Id == menuId, cancellationToken);
            return menu;
        }
        public async Task<List<MenuItem>> PublicGetByMenu(int menuId, CancellationToken cancellationToken)
        {
            var menuItems = await _context.MenuItems.Include(x => x.Descriptions).Where(x => x.MenuId == menuId && x.Active).OrderBy(x => x.Order).ToListAsync(cancellationToken);
            return menuItems;
        }

        public async Task<MenuItem> Add(MenuItem menuItem, CancellationToken cancellationToken)
        { 
            await _context.MenuItems.AddAsync(menuItem, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return menuItem;
        }

        public async Task<MenuItem> Get(int menuItemId, CancellationToken cancellationToken)
        {

            var menuItem = await _context.MenuItems.AsNoTracking().FirstOrDefaultAsync(x => x.Id == menuItemId, cancellationToken);
            if (menuItem == null) return null;
            {
                var menuItemDescriptions = await _context.MenuItemDescriptions.AsNoTracking().Where(x => x.MenuItemId == menuItemId).ToListAsync(cancellationToken);
                if (menuItemDescriptions != null)
                {
                    menuItem.Descriptions = menuItemDescriptions;
                }
            }
            return menuItem;

        }
        public async Task<List<MenuItem>> GetAll(CancellationToken cancellationToken)
        {
            var menuItems = await _context.MenuItems.OrderBy(x => x.Order).ToListAsync(cancellationToken);
            if (menuItems != null)
            {
                foreach (var menuItem in menuItems)
                {
                    var menuItemDescriptions = await _context.MenuItemDescriptions.Where(x => x.MenuItemId == menuItem.Id).ToListAsync(cancellationToken);
                    if (menuItemDescriptions != null)
                    {
                        menuItem.Descriptions = menuItemDescriptions;
                    }
                }
            }
            return menuItems;
        }

        public async Task<MenuItem> Update(MenuItem menuItem, CancellationToken cancellationToken)
        {
            var menuItemDescription = _context.MenuItemDescriptions.AsNoTracking().ToList();
            var menuItemDescriptionToAdd = new List<MenuItemDescription>(); // List dùng để thêm mới nếu chưa tồn tại
            var listMenuItemDescription = menuItem.Descriptions.ToList();
            foreach (var item in listMenuItemDescription)
            {
                // Kiểm tra tồn tại trong DB
                var itemExist = menuItemDescription.FirstOrDefault(x => x.LanguageCode == item.LanguageCode && x.MenuItemId == menuItem.Id);
                if (itemExist == null)
                {
                    item.MenuItemId = menuItem.Id;
                    menuItemDescriptionToAdd.Add(item);
                    menuItem.Descriptions.Remove(item);
                }
                else { item.Id = itemExist.Id; }
            }
            _context.MenuItems.Update(menuItem);
            await _context.MenuItemDescriptions.AddRangeAsync(menuItemDescriptionToAdd, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return menuItem;
        }

        public async Task<List<MenuItem>> GetChildrens(int menuId, CancellationToken cancellationToken)
        {
            var menuItems = await _context.MenuItems.Include(x => x.Descriptions).Where(x => x.ParentId == menuId).ToListAsync(cancellationToken);
           
            return menuItems;
        }

        public async Task<List<MenuItem>> GetMenuItemsByListId(List<int> listMenuId, CancellationToken cancellationToken)
        {
            var menuItems = new List<MenuItem>();
            foreach (var id in listMenuId)
            {
                var menuItem = await _context.MenuItems.Include(x => x.Descriptions).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
                if (menuItem != null)
                    menuItems.Add(menuItem);
            }
            return menuItems;
        }

        public Task Delete(MenuItem menuItem, CancellationToken cancellationToken)
        {
            _context.MenuItems.Remove(menuItem);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task DeleteRange(List<MenuItem> menuItem, CancellationToken cancellationToken)
        {
            _context.MenuItems.RemoveRange(menuItem);
            _context.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
