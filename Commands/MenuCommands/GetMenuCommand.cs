namespace ApiTemplate.Commands.MenuCommands
{
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Repositories;
    using System.Threading;
    using System.Threading.Tasks;
    using ViewModels.MenuViewModels;

    public class GetMenuCommand : IGetMenuCommand
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper<Models.Menu, AdminMenu> _menuMapper;

        public GetMenuCommand(
            IMenuRepository menuRepository,
            IMapper<Models.Menu, AdminMenu> menuMapper)
        {
            _menuRepository = menuRepository;
            _menuMapper = menuMapper;
        }

        public async Task<IActionResult> ExecuteAsync(int menuId, CancellationToken cancellationToken)
        {
            var menu = await _menuRepository.Get(menuId, cancellationToken);
            if (menu == null) return new NoContentResult();

            var menuViewModel = _menuMapper.Map(menu);

            return new OkObjectResult(menuViewModel);
        }
    }
}
