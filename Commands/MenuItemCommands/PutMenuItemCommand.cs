namespace ApiTemplate.Commands.MenuItemCommands
{
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Repositories;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using ViewModels.MenuItemViewModels;
    public class PutMenuItemCommand : IPutMenuItemCommand
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMapper<Models.MenuItem, MenuItem> _menuItemToMenuItemMapper;
        private readonly IMapper<SaveMenuItem, Models.MenuItem> _saveMenuItemToMenuItemMapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PutMenuItemCommand(
            IMenuItemRepository menuItemRepository,
            IMapper<Models.MenuItem, MenuItem> menuItemToMenuItemMapper,
            IMapper<SaveMenuItem, Models.MenuItem> saveMenuItemToMenuItemMapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _menuItemRepository = menuItemRepository;
            _menuItemToMenuItemMapper = menuItemToMenuItemMapper;
            _saveMenuItemToMenuItemMapper = saveMenuItemToMenuItemMapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> ExecuteAsync(int menuItemId, SaveMenuItem saveMenuItem, CancellationToken cancellationToken)
        {
            var menuItem = await _menuItemRepository.Get(menuItemId, cancellationToken);
            if (menuItem == null)
            {
                return new NotFoundResult(); 
            }
            _saveMenuItemToMenuItemMapper.Map(saveMenuItem, menuItem);

            var listMenuItemId = await getListMenuItemId(new List<int>(), menuItem.Id, cancellationToken);
            var menuItems = await _menuItemRepository.GetMenuItemsByListId(listMenuItemId, cancellationToken);

            //var user = _httpContextAccessor.HttpContext.User;
            //if (user == null)
            //    return new NotFoundResult();

            //var claims = user.Claims.ToList();
            //if (claims.Count < 1)
            //    return new NotFoundResult();
            //// Lấy Id của người dùng
            //var userId = claims.FirstOrDefault(claimRecord => claimRecord.Type == "sub").Value;

            foreach (var item in menuItems)
            {
                item.Active = saveMenuItem.Active;
                //item.ModifiedBy = userId;
                await _menuItemRepository.Update(item, cancellationToken);
            }
            var menuItemViewModel = _menuItemToMenuItemMapper.Map(menuItem);

            return new OkObjectResult(menuItemViewModel);
        }

        public async Task<List<int>> getListMenuItemId(List<int> listMenuItemId, int menuItemId, CancellationToken cancellationToken)
        {
            var menuItem = await _menuItemRepository.Get(menuItemId, cancellationToken);
            if (menuItem != null)
            {
                listMenuItemId.Add(menuItemId);
                var childrens = await _menuItemRepository.GetChildrens(menuItemId, cancellationToken);
                foreach (var item in childrens)
                {
                    await getListMenuItemId(listMenuItemId, item.Id, cancellationToken);
                }
            }
            return listMenuItemId;
        }
    }
}
