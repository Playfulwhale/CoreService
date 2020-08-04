namespace ApiTemplate.Repositories
{
    using Data;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class SystemSettingRepository : ISystemSettingRepository
    {
        private readonly DataContext _context;

        public SystemSettingRepository(DataContext context)
        {
            _context = context;
        }

        public Task<SystemSetting> Add(SystemSetting systemSetting, CancellationToken cancellationToken)
        {
            _context.SystemSettings.Add(systemSetting);
            _context.SaveChanges();
            return Task.FromResult(systemSetting);
        }

        public Task<List<SystemSetting>> AddList(List<SystemSetting> systemSettings, CancellationToken cancellationToken)
        {
            foreach (var item in systemSettings)
            {
                if(_context.SystemSettings.FirstOrDefault(x => x.Key == item.Key) == null)
                {
                    _context.SystemSettings.Add(item);
                }
                else
                {
                    var existingSystemSetting = _context.SystemSettings.FirstOrDefault(x => x.Key == item.Key);
                    if (existingSystemSetting == null) continue;
                    existingSystemSetting.Name = item.Name;
                    existingSystemSetting.Value = item.Value;
                    existingSystemSetting.Data = item.Data;
                    existingSystemSetting.GroupId = item.GroupId;
                    existingSystemSetting.DataType = item.DataType;
                    existingSystemSetting.Description = item.Description;
                    _context.SystemSettings.Update(existingSystemSetting);
                }
            }
            _context.SaveChanges();
            return Task.FromResult(systemSettings);
        }

        public Task<SystemSetting> Update(SystemSetting systemSetting, CancellationToken cancellationToken)
        {
            var existingSystemSetting = _context.SystemSettings.FirstOrDefault(x => x.Id == systemSetting.Id);
            existingSystemSetting.Name = systemSetting.Name;
            existingSystemSetting.Key = systemSetting.Key;
            existingSystemSetting.Value = systemSetting.Value;
            existingSystemSetting.Data = systemSetting.Data;
            existingSystemSetting.DataType = systemSetting.DataType;
            existingSystemSetting.Description = systemSetting.Description;
            existingSystemSetting.GroupId = systemSetting.GroupId;
            existingSystemSetting.ModifiedBy = systemSetting.ModifiedBy;
            existingSystemSetting.ModifiedAt = systemSetting.ModifiedAt;
            _context.SaveChanges();
            return Task.FromResult(existingSystemSetting);
        }

        public Task Delete(SystemSetting systemSetting, CancellationToken cancellationToken)
        {
            if (!_context.SystemSettings.Contains(systemSetting)) return Task.CompletedTask;
            _context.SystemSettings.Remove(systemSetting);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task<SystemSetting> Get(int id, CancellationToken cancellationToken)
        {
            var systemSetting = _context.SystemSettings.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(systemSetting);
        }

        public Task<SystemSetting> GetByKey(string key, CancellationToken cancellationToken)
        {
            var systemSetting = _context.SystemSettings.FirstOrDefault(x => x.Key == key);
            return Task.FromResult(systemSetting);
        }

        public Task<List<SystemSetting>> GetAll(CancellationToken cancellationToken)
        {
            var list = _context.SystemSettings.ToList();
            return Task.FromResult(list);
        }
    }
}