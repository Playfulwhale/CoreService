namespace ApiTemplate.Commands.MenuCommands
{
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Repositories;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using ViewModels.MenuViewModels;
    public class PutMenuCommand : IPutMenuCommand
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper<Models.Menu, Menu> _menuToMenuMapper;
        private readonly IMapper<SaveMenu, Models.Menu> _saveMenuToMenuMapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PutMenuCommand(
            IMenuRepository menuRepository,
            IMapper<Models.Menu, Menu> menuToMenuMapper,
            IMapper<SaveMenu, Models.Menu> saveMenuToMenuMapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _menuRepository = menuRepository;
            _menuToMenuMapper = menuToMenuMapper;
            _saveMenuToMenuMapper = saveMenuToMenuMapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> ExecuteAsync(int menuId, SaveMenu saveMenu, CancellationToken cancellationToken)
        {
            var menu = await _menuRepository.Get(menuId, cancellationToken);
            if (menu == null)
            {
                return new NotFoundResult();
            }
            _saveMenuToMenuMapper.Map(saveMenu, menu);

            //var user = _httpContextAccessor.HttpContext.User;
            //if (user == null)
            //    return new NotFoundResult();

            //var claims = user.Claims.ToList();
            //if (claims.Count < 1)
            //    return new NotFoundResult();

            //var userId = claims.FirstOrDefault(claimRecord => claimRecord.Type == "sub").Value;

            //menu.ModifiedBy = userId;

            menu = await _menuRepository.Update(menu, cancellationToken);
            var menuViewModel = _menuToMenuMapper.Map(menu);

            return new OkObjectResult(menuViewModel);
        }
    }
}
