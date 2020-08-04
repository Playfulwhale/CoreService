namespace ApiTemplate.Commands.MenuItemCommands
{
    using Repositories;
    using ViewModels.MenuItemViewModels;
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetMenuItemCommand : IGetMenuItemCommand
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMapper<Models.MenuItem, MenuItem> _menuItemMapper;

        public GetMenuItemCommand(
            IMenuItemRepository menuItemRepository,
            IMapper<Models.MenuItem, MenuItem> menuItemMapper)
        {
            _menuItemRepository = menuItemRepository;
            _menuItemMapper = menuItemMapper;
        }

        public async Task<IActionResult> ExecuteAsync(int menuItemId, CancellationToken cancellationToken)
        {
            var menuItem = await _menuItemRepository.Get(menuItemId, cancellationToken);
            if (menuItem == null)
            {
               return new NoContentResult();
            }

            var menuViewModel = _menuItemMapper.Map(menuItem);
            return new OkObjectResult(menuViewModel);
        }
    }
}
