namespace ApiTemplate.Repositories
{
    using Data;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class SystemSettingGroupRepository : ISystemSettingGroupRepository
    {
        private readonly DataContext _context;

        public SystemSettingGroupRepository(DataContext context)
        {
            _context = context;
        }

        public Task<SystemSettingGroup> Add(SystemSettingGroup systemSettingGroup, CancellationToken cancellationToken)
        {
            _context.SystemSettingGroups.Add(systemSettingGroup);
            _context.SaveChanges();
            return Task.FromResult(systemSettingGroup);
        }

        public Task<SystemSettingGroup> Update(SystemSettingGroup systemSettingGroup, CancellationToken cancellationToken)
        {
            var existingSystemSettingGroup = _context.SystemSettingGroups.FirstOrDefault(x => x.Id == systemSettingGroup.Id);
            existingSystemSettingGroup.Name = systemSettingGroup.Name;
            existingSystemSettingGroup.Description = systemSettingGroup.Description;
            existingSystemSettingGroup.Order = systemSettingGroup.Order;
            existingSystemSettingGroup.ModifiedBy = systemSettingGroup.ModifiedBy;
            existingSystemSettingGroup.ModifiedAt = systemSettingGroup.ModifiedAt;
            _context.SaveChanges();
            return Task.FromResult(existingSystemSettingGroup);
        }

        public Task Delete(SystemSettingGroup systemSettingGroup, CancellationToken cancellationToken)
        {
            if (_context.SystemSettingGroups.Contains(systemSettingGroup))
            {
                _context.SystemSettingGroups.Remove(systemSettingGroup);
                _context.SaveChanges();
            }
            return Task.CompletedTask;
        }

        public Task<SystemSettingGroup> Get(int id, CancellationToken cancellationToken)
        {
            var systemSettingGroup = _context.SystemSettingGroups.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(systemSettingGroup);
        }

        public Task<List<SystemSettingGroup>> GetAll(CancellationToken cancellationToken)
        {
            var list = _context.SystemSettingGroups.ToList();
            return Task.FromResult(list);
        }
    }
}