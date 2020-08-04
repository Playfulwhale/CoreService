namespace ApiTemplate.Commands.MenuItemCommands
{
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Constants;
    using Repositories;
    using ViewModels.MenuItemViewModels;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using System.Linq;

    public class PostMenuItemCommand : IPostMenuItemCommand
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMapper<Models.MenuItem, MenuItem> _menuItemToMenuItemMapper;
        private readonly IMapper<SaveMenuItem, Models.MenuItem> _saveMenuItemToMenuItemMapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PostMenuItemCommand(
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

        public async Task<IActionResult> ExecuteAsync(int menuId, SaveMenuItem saveMenuItem, CancellationToken cancellationToken)
        {
            var menu = await _menuItemRepository.GetMenuId(menuId, cancellationToken);

            if (menu == null)
            {
                return new NoContentResult();
            }
            saveMenuItem.MenuId = menuId;
            var menuItem = _saveMenuItemToMenuItemMapper.Map(saveMenuItem);

            //var user = _httpContextAccessor.HttpContext.User;
            //if (user == null)
            //    return new NotFoundResult();

            //var claims = user.Claims.ToList();
            //if (claims.Count < 1)
            //    return new NotFoundResult();

            //var userId = claims.FirstOrDefault(claimRecord => claimRecord.Type == "sub").Value;
            //menuItem.CreatedBy = userId;
            //menuItem.ModifiedBy = userId;

            menuItem = await _menuItemRepository.Add(menuItem, cancellationToken);
            var menuItemViewModel = _menuItemToMenuItemMapper.Map(menuItem);
            
            return new CreatedAtRouteResult( MenuItemsControllerRoute.GetMenuItem,
                    new { id = menuItemViewModel.Id },
                    menuItemViewModel);

        }
    }
}