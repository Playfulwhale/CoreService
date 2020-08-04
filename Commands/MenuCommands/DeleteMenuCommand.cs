namespace ApiTemplate.Commands.MenuCommands
{
    using Microsoft.AspNetCore.Mvc;
    using Repositories;
    using System.Threading;
    using System.Threading.Tasks;

    public class DeleteMenuCommand : IDeleteMenuCommand
    {
        private readonly IMenuRepository _menuRepository;

        public DeleteMenuCommand(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<IActionResult> ExecuteAsync(int menuId, CancellationToken cancellationToken)
        {
            var menu = await _menuRepository.Get(menuId, cancellationToken);
            if (menu == null)
            {
                return new NotFoundResult();
            }

            await _menuRepository.Delete(menu, cancellationToken);

            return new NoContentResult();
        }
    }
}