namespace ApiTemplate.Repositories
{
    using Models;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ISystemSettingRepository
    {
        Task<SystemSetting> Add(SystemSetting systemSetting, CancellationToken cancellationToken);

        Task<SystemSetting> Update(SystemSetting systemSetting, CancellationToken cancellationToken);

        Task Delete(SystemSetting systemSetting, CancellationToken cancellationToken);

        Task<SystemSetting> Get(int id, CancellationToken cancellationToken);

        Task<SystemSetting> GetByKey(string key, CancellationToken cancellationToken);

        Task<List<SystemSetting>> GetAll(CancellationToken cancellationToken);

        Task<List<SystemSetting>> AddList(List<SystemSetting> systemSettings, CancellationToken cancellationToken);
    }
}