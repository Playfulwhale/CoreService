namespace ApiTemplate.Commands.MenuItemCommands
{
    using Repositories;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public class DeleteMenuItemCommand : IDeleteMenuItemCommand
    {
        private readonly IMenuItemRepository _menuItemRepository;

        public DeleteMenuItemCommand(IMenuItemRepository menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
        }

        public async Task<IActionResult> ExecuteAsync(int menuItemId, CancellationToken cancellationToken)
        {
            var menuItem = await _menuItemRepository.Get(menuItemId, cancellationToken);
            if (menuItem == null)
            {
                return new NoContentResult();
            }

            var listMenuItemId = await getListMenuItemId(new List<int>(), menuItem.Id, cancellationToken);
            var menuItems = await _menuItemRepository.GetMenuItemsByListId(listMenuItemId, cancellationToken);
            await _menuItemRepository.DeleteRange(menuItems, cancellationToken);
            return new NoContentResult();
        }

        public async Task<List<int>> getListMenuItemId(List<int> listMenuItemId, int menuItemId, CancellationToken cancellationToken)
        {
            var menuItem = await _menuItemRepository.Get(menuItemId, cancellationToken);
            if (menuItem != null)
            {
                listMenuItemId.Add(menuItemId);
                var childrens = await _menuItemRepository.GetChildrens(menuItemId, cancellationToken);
                foreach(var item in childrens)
                {
                    await getListMenuItemId(listMenuItemId, item.Id, cancellationToken);
                }
            }
            return listMenuItemId;
        }
    }
}