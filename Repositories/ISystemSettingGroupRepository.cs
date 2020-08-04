namespace ApiTemplate.Repositories
{
    using Models;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ISystemSettingGroupRepository
    {
        Task<SystemSettingGroup> Add(SystemSettingGroup systemSettingGroup, CancellationToken cancellationToken);

        Task<SystemSettingGroup> Update(SystemSettingGroup systemSettingGroup, CancellationToken cancellationToken);

        Task Delete(SystemSettingGroup systemSettingGroup, CancellationToken cancellationToken);

        Task<SystemSettingGroup> Get(int id, CancellationToken cancellationToken);

        Task<List<SystemSettingGroup>> GetAll(CancellationToken cancellationToken);
    }
}