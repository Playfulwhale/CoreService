namespace ApiTemplate.Commands.MenuCommands
{
    using Constants;
    using Repositories;
    using ViewModels.MenuViewModels;
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Linq;

    public class PostMenuCommand : IPostMenuCommand
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper<Models.Menu, Menu> _menuToMenuMapper;
        private readonly IMapper<SaveMenu, Models.Menu> _saveMenuToMenuMapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PostMenuCommand(
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

        public async Task<IActionResult> ExecuteAsync(SaveMenu saveMenu, CancellationToken cancellationToken)
        {
            var menu = _saveMenuToMenuMapper.Map(saveMenu);

            //var user = _httpContextAccessor.HttpContext.User;
            //if (user == null)
            //    return new NotFoundResult();

            //var claims = user.Claims.ToList();
            //if (claims.Count < 1)
            //    return new NotFoundResult();

            //var userId = claims.FirstOrDefault(claimRecord => claimRecord.Type == "sub").Value;

            //if (menu.CreatedBy == null)
            //    menu.CreatedBy = userId;
            //menu.ModifiedBy = userId;


            menu = await _menuRepository.Add(menu, cancellationToken);
            var menuViewModel = _menuToMenuMapper.Map(menu);

            return new CreatedAtRouteResult(
                MenusControllerRoute.GetMenu,
                new { id = menuViewModel.Id },
                menuViewModel);
        }
    }
}
