namespace ApiTemplate.Commands.MenuCommands
{
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Repositories;
    using System.Threading;
    using System.Threading.Tasks;
    using ViewModels.MenuViewModels;

    public class GetMenuAllCommand : IGetMenuAllCommand
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper<Models.Menu, AdminMenu> _menuMapper;

        public GetMenuAllCommand(
            IMenuRepository menuRepository,
            IMapper<Models.Menu, AdminMenu> menuMapper)
        {
            _menuRepository = menuRepository;
            _menuMapper = menuMapper;
        }

        public async Task<IActionResult> ExecuteAsync(CancellationToken cancellationToken)
        {
            var menus = await _menuRepository.GetAll(cancellationToken);
            if (menus == null) return new NoContentResult();

            var menuViewModel = _menuMapper.MapList(menus);

            return new OkObjectResult(menuViewModel);
        }
    }
}
