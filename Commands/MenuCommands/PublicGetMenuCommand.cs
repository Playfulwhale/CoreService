namespace ApiTemplate.Commands.MenuCommands
{
    using Repositories;
    using ViewModels.MenuViewModels;
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Linq;

    public class PublicGetMenuCommand : IPublicGetMenuCommand
    {
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IMenuRepository _menuRepository;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMapper<Models.Menu, PublicMenu> _menuItemMapper;

        public PublicGetMenuCommand(
            IActionContextAccessor actionContextAccessor,
            IMenuRepository menuRepository,
            IMenuItemRepository menuItemRepository,
            IMapper<Models.Menu, PublicMenu> menuItemMapper)
        {
            _actionContextAccessor = actionContextAccessor;
            _menuRepository = menuRepository;
            _menuItemRepository = menuItemRepository;
            _menuItemMapper = menuItemMapper;
        }

        public async Task<IActionResult> ExecuteAsync(string position, CancellationToken cancellationToken)
        {
            var menu = await _menuRepository.GetByPosition(position, cancellationToken);
            if (menu == null) return new NoContentResult();

            var menuItems = await _menuItemRepository.PublicGetByMenu(menu.Id, cancellationToken);

            // Lọc theo mã ngôn ngữ
            var httpContext = _actionContextAccessor.ActionContext.HttpContext;
            var languageCode = httpContext.Request.Headers["Accept-Language"].ToString().Split(',').FirstOrDefault();
            foreach (var item in menuItems)
            {
                item.Descriptions = item.Descriptions.Where(x => x.LanguageCode == languageCode).ToList();
                if(item.Descriptions.Count == 0)
                {
                    menu.menuItems.Remove(item);
                }
            }
            
            var menuViewModel = _menuItemMapper.Map(menu);
         
            return new OkObjectResult(menuViewModel);
        }
    }
}
