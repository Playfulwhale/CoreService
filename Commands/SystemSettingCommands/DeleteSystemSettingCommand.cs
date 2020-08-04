namespace ApiTemplate.Commands.SystemSettingCommands
{
    using Repositories;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading;
    using System.Threading.Tasks;

    public class DeleteSystemSettingCommand : IDeleteSystemSettingCommand
    {
        private readonly ISystemSettingRepository _systemSettingRepository;

        public DeleteSystemSettingCommand(ISystemSettingRepository systemSettingRepository)
        {
            _systemSettingRepository = systemSettingRepository;
        }

        public async Task<IActionResult> ExecuteAsync(string key, CancellationToken cancellationToken)
        {
            var systemSetting = await _systemSettingRepository.GetByKey(key, cancellationToken);
            if (systemSetting == null)
            {
                return new NotFoundResult();
            }

            await _systemSettingRepository.Delete(systemSetting, cancellationToken);

            return new NoContentResult();
        }
    }
}